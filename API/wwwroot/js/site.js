(() => {
  const app = document.querySelector("[data-payment-app]");
  if (!app) {
    return;
  }

  const orderCodeInput = app.querySelector("[data-order-code]");
  const amountInput = app.querySelector("[data-amount]");
  const providerInput = app.querySelector("[data-provider]");
  const createButton = app.querySelector("[data-create-payment]");
  const webhookSuccessButton = app.querySelector("[data-webhook-success]");
  const webhookFailedButton = app.querySelector("[data-webhook-failed]");

  const createStatus = app.querySelector("[data-create-status]");
  const webhookStatus = app.querySelector("[data-webhook-status]");
  const requestIdView = app.querySelector("[data-request-id]");
  const orderView = app.querySelector("[data-view-order]");
  const providerView = app.querySelector("[data-view-provider]");
  const paymentUrlView = app.querySelector("[data-payment-url]");
  const requestJsonView = app.querySelector("[data-request-json]");
  const webhookJsonView = app.querySelector("[data-webhook-json]");

  let paymentState = null;

  const setStatus = (element, message, type = "") => {
    element.textContent = message;
    element.classList.remove("is-success", "is-error");

    if (type) {
      element.classList.add(type);
    }
  };

  const setWebhookButtons = (enabled) => {
    webhookSuccessButton.disabled = !enabled;
    webhookFailedButton.disabled = !enabled;
  };

  const buildCreatePayload = () => ({
    orderCode: orderCodeInput.value.trim(),
    amount: Number(amountInput.value),
    provider: providerInput.value
  });

  const renderCreatePayload = () => {
    const payload = buildCreatePayload();
    requestJsonView.textContent = JSON.stringify(payload, null, 2);
  };

  const resetWebhookView = () => {
    webhookJsonView.textContent = JSON.stringify({
      status: "-",
      code: "-",
      message: "-"
    }, null, 2);
    setStatus(webhookStatus, "Chưa nhận webhook.");
    setWebhookButtons(Boolean(paymentState));
  };

  const createPayment = async () => {
    const payload = buildCreatePayload();
    renderCreatePayload();

    setStatus(createStatus, "Đang tạo request thanh toán...");
    setWebhookButtons(false);

    try {
      const response = await fetch("/api/payment/create", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
      });

      const data = await response.json();

      if (!response.ok) {
        throw new Error(data.message || "Tạo thanh toán thất bại.");
      }

      paymentState = data;
      requestIdView.textContent = data.requestId;
      orderView.textContent = data.orderCode;
      providerView.textContent = data.provider;
      paymentUrlView.textContent = data.paymentUrl;

      setStatus(createStatus, `${data.message} Request ID: ${data.requestId}`, "is-success");
      resetWebhookView();
    } catch (error) {
      paymentState = null;
      requestIdView.textContent = "-";
      orderView.textContent = "-";
      providerView.textContent = "-";
      paymentUrlView.textContent = "-";
      setStatus(createStatus, error.message, "is-error");
      resetWebhookView();
    }
  };

  const sendWebhook = async (success) => {
    if (!paymentState) {
      return;
    }

    setStatus(webhookStatus, "Đang gửi webhook về server...");
    setWebhookButtons(false);

    try {
      const response = await fetch("/api/payment/webhook", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          requestId: paymentState.requestId,
          orderCode: paymentState.orderCode,
          provider: paymentState.provider,
          success
        })
      });

      const data = await response.json();

      if (!response.ok) {
        throw new Error(data.message || "Webhook thất bại.");
      }

      webhookJsonView.textContent = JSON.stringify(data, null, 2);
      setStatus(
        webhookStatus,
        `Webhook trả về status ${data.status} với code ${data.code}.`,
        success ? "is-success" : "is-error"
      );
      setWebhookButtons(true);
    } catch (error) {
      setStatus(webhookStatus, error.message, "is-error");
      setWebhookButtons(true);
    }
  };

  orderCodeInput.addEventListener("input", renderCreatePayload);
  amountInput.addEventListener("input", renderCreatePayload);
  providerInput.addEventListener("change", renderCreatePayload);
  createButton.addEventListener("click", createPayment);
  webhookSuccessButton.addEventListener("click", () => sendWebhook(true));
  webhookFailedButton.addEventListener("click", () => sendWebhook(false));

  renderCreatePayload();
  resetWebhookView();
})();
