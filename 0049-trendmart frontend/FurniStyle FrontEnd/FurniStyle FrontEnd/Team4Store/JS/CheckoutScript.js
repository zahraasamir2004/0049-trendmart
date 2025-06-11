document.addEventListener("DOMContentLoaded", function () {
  const placeOrderBtn = document.getElementById("placeOrder");
  const countryInput = document.getElementById("countryInput");
  const cityInput = document.getElementById("cityInput");
  const addressInput = document.getElementById("adressInput");
  const detailsInput = document.getElementById("detailsInput");
  const btnText = document.getElementById("btnText");
  const loadingSpinner = document.getElementById("loadingSpinner");
  const successMessage = document.getElementById("successMessage");
  const mainSection = document.getElementById("mainSection");

  const touched = {
    countryInput: false,
    cityInput: false,
    addressInput: false,
    detailsInput: false,
  };

  function validateForm(showErrors = true) {
    let isValid = true;
    const fields = [
      { input: countryInput, minLength: 3, errorId: "countryError" },
      { input: cityInput, minLength: 3, errorId: "cityError" },
      { input: addressInput, minLength: 5, errorId: "adressError" },
      { input: detailsInput, minLength: 5, errorId: "detailsError" },
    ];

    fields.forEach(({ input, minLength, errorId }) => {
      const value = input.value.trim();
      const errorElement = document.getElementById(errorId);
      const inputTouched = touched[input.id];
      if (value.length < minLength) {
        isValid = false;
        if (inputTouched && showErrors) {
          errorElement.style.display = "block";
        }
      } else {
        errorElement.style.display = "none";
      }
    });

    placeOrderBtn.disabled = !isValid;
    placeOrderBtn.style.opacity = isValid ? "1" : "0.3";
    placeOrderBtn.style.cursor = isValid ? "pointer" : "not-allowed";
  }

  [countryInput, cityInput, addressInput, detailsInput].forEach((input) => {
    input.addEventListener("input", () => {
      touched[input.id] = true;
      validateForm(true);
    });
    input.addEventListener("blur", () => {
      touched[input.id] = true;
      validateForm(true);
    });
  });

  function loadUserData() {
    const storedData = localStorage.getItem("userCredentials");
    if (storedData) {
      try {
        const userData = JSON.parse(storedData);
        document.getElementById("nameInput").value = userData.name || "";
        document.getElementById("emailInput").value = userData.email || "";
        document.getElementById("mobileInput").value =
          userData.phoneNumber || "";
      } catch (error) {
        console.error("Error parsing userCredentials:", error);
      }
    }
  }

  function loadCart() {
    const storedCart = localStorage.getItem("cart");
    let cart = [];
    if (storedCart) {
      try {
        cart = JSON.parse(storedCart);
      } catch (error) {
        console.error("Error parsing cart data:", error);
      }
    }
    const cartItemsContainer = document.getElementById("cartItems");
    const subtotalElement = document.getElementById("subtotal");
    const totalElement = document.getElementById("total");
    let subtotal = 0;

    if (cart.length > 0) {
      const cartItemsHTML = cart
        .map((item) => {
          const itemPrice = parseFloat(item.price);
          const itemTotal = itemPrice * item.quantity;
          subtotal += itemTotal;
          return `
                      <div class="row mb-3">
                          <div class="col-2">
                    <img src="${item.image}" alt="${
            item.name
          }" class="img-fluid" />
                          </div>
                          <div class="col-6">${item.name} (x${
            item.quantity
          })</div>
                          <div class="col-4 text-end">$${itemTotal.toFixed(
                            2
                          )}</div>
                      </div>
                  `;
        })
        .join("");
      cartItemsContainer.innerHTML = cartItemsHTML;
      subtotalElement.textContent = `$${subtotal.toFixed(2)}`;
      totalElement.textContent = `$${subtotal.toFixed(2)}`;
    } else {
      cartItemsContainer.innerHTML = `<p class="text-center text-muted">Your cart is empty.</p>`;
    }
    return cart;
  }

  function generateOrderNumber() {
    const random = Math.floor(1000 + Math.random() * 9000);
    return `am-${random} `;
  }

  function displayOrderDetails() {
    const orders = JSON.parse(localStorage.getItem("orderDetails") || "[]");
    const latestOrder = orders[orders.length - 1];
    if (!latestOrder) {
      console.error("No order details found in localStorage");
      return;
    }

    document.getElementById("orderNumber").textContent =
      latestOrder.orderNumber;
    document.getElementById("orderDate").textContent = new Date(
      latestOrder.orderDate
    ).toLocaleString();

    const orderStatusElement = document.getElementById("orderStatus");
    orderStatusElement.textContent = latestOrder.status;

    const urlParams = new URLSearchParams(window.location.search);
    const paymentStatus = urlParams.get("payment");
    const cardType = urlParams.get("type");
    const cardLastFour = urlParams.get("card");

    if (paymentStatus === "success" && latestOrder.payment) {
      const paymentInfo = document.createElement("p");
      paymentInfo.innerHTML = `<br> Paid with ${cardType || "Card"} ending in ${
        cardLastFour || latestOrder.payment.cardNumber || ""
      } <br>`;
      // Insert payment info after order status within the same container
      orderStatusElement.parentNode.insertBefore(
        paymentInfo,
        orderStatusElement.nextSibling
      );
    }

    document.getElementById("userEmail").textContent = latestOrder.user.email;
    document.getElementById("userEmailDetail").textContent =
      latestOrder.user.email;
    document.getElementById("userName").textContent = latestOrder.user.name;
    document.getElementById("userPhone").textContent =
      latestOrder.user.phoneNumber;

    document.getElementById("shippingCountry").textContent =
      latestOrder.shipping.country;
    document.getElementById("shippingCity").textContent =
      latestOrder.shipping.city;
    document.getElementById("shippingAddress").textContent =
      latestOrder.shipping.address;
    document.getElementById("shippingDetails").textContent =
      latestOrder.shipping.details;

    const itemsContainer = document.getElementById("orderItems");
    itemsContainer.innerHTML = latestOrder.items
      .map(
        (item) => `
              <div class="d-flex align-items-center mb-3 border-bottom pb-2">
                  <img src="${item.image}" alt="${
          item.name
        }" class="img-fluid me-3" style="width: 60px;">
                  <div class="flex-grow-1">
                      <p class="mb-1">${item.name} (x${item.quantity})</p>
                      <p class="mb-0 text-muted">$${item.price.toFixed(
                        2
                      )} each</p>
                  </div>
                  <p class="mb-0">$${(item.price * item.quantity).toFixed(
                    2
                  )}</p>
              </div>
          `
      )
      .join("");

    document.getElementById(
      "orderSubtotal"
    ).textContent = `$${latestOrder.subtotal.toFixed(2)}`;
    document.getElementById(
      "orderTotal"
    ).textContent = `$${latestOrder.total.toFixed(2)}`;
  }

  function checkPaymentStatus() {
    const urlParams = new URLSearchParams(window.location.search);
    const paymentStatus = urlParams.get("payment");

    if (paymentStatus === "success") {
      successMessage.classList.remove("d-none");
      mainSection.style.display = "none";

      countryInput.value = "";
      cityInput.value = "";
      addressInput.value = "";
      detailsInput.value = "";
      btnText.style.display = "inline";
      loadingSpinner.style.display = "none";
      validateForm(false);
      Object.keys(touched).forEach((key) => (touched[key] = false));

      displayOrderDetails();

      window.history.replaceState({}, document.title, window.location.pathname);
    }
  }

  placeOrderBtn.addEventListener("click", function () {
    if (!placeOrderBtn.disabled) {
      const cart = loadCart();

      if (cart.length === 0) {
        alert("Your cart is empty. Please add items to proceed.");
        return;
      }

      btnText.style.display = "none";
      loadingSpinner.style.display = "inline-block";
      placeOrderBtn.disabled = true;

      const orderDetails = {
        orderNumber: generateOrderNumber(),
        user: {
          name: document.getElementById("nameInput").value,
          email: document.getElementById("emailInput").value,
          phoneNumber: document.getElementById("mobileInput").value,
        },
        shipping: {
          country: countryInput.value.trim(),
          city: cityInput.value.trim(),
          address: addressInput.value.trim(),
          details: detailsInput.value.trim(),
        },
        items: cart.map((item) => ({
          id: item.id,
          name: item.name,
          price: parseFloat(item.price),
          image: item.image,
          quantity: item.quantity,
        })),
        subtotal: parseFloat(
          document.getElementById("subtotal").textContent.replace("$", "")
        ),
        total: parseFloat(
          document.getElementById("total").textContent.replace("$", "")
        ),
        orderDate: new Date().toISOString(),
        status: "Pending",
      };

      localStorage.setItem("pendingOrder", JSON.stringify(orderDetails));

      setTimeout(() => {
        window.location.href = "payment.html";
      }, 3000);
    }
  });

  loadUserData();
  loadCart();
  validateForm(false);
  checkPaymentStatus();
});

const userCredentials = JSON.parse(localStorage.getItem("userCredentials"));

if (!userCredentials) {
  alert("not logged in .. you must be logged in to complete your checkout");
  window.location.href = "/Login.html";
} else {
  const loginNav = document.getElementById("loginNavItem");
  const registerNav = document.getElementById("registerNavItem");
  if (loginNav) loginNav.style.display = "none";
  if (registerNav) registerNav.style.display = "none";

  const userNav = document.getElementById("userNavItem");
  const userNameDisplay = document.getElementById("userNameDisplay");

  if (userNav && userNameDisplay) {
    userNameDisplay.textContent = `Welcome, ${userCredentials.name}`;
    userNav.classList.remove("d-none");
  }
}
