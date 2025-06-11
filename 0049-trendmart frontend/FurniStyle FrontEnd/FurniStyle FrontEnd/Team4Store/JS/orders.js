const ordersContainer = document.getElementById("ordersContainer");
const alertBox = document.getElementById("alertBox");

function showAlert(message) {
  alertBox.textContent = message;
  alertBox.classList.remove("d-none");
  alertBox.classList.add("show");

  setTimeout(() => {
    alertBox.classList.remove("show");
    alertBox.classList.add("d-none");
  }, 3000);
}

function renderOrders() {
  let orderData = JSON.parse(localStorage.getItem("orderDetails")) || [];
  ordersContainer.innerHTML = "";

  if (orderData.length === 0) {
    ordersContainer.innerHTML = `<div class="col-12"><p class="text-muted">No orders found in localStorage.</p></div>`;
    return;
  }

  orderData.forEach((order, orderIndex) => {
    order.items.forEach((item, itemIndex) => {
      const isReturned = item.returned === true;
      const opacityClass = isReturned ? "opacity-50" : "";
      const statusText = isReturned ? "Returned" : order.status;
      const disabledBtn =
        isReturned || order.status !== "Success" ? "disabled" : "";

      const orderHTML = `
        <div class="col-md-4">
          <div class="card shadow-sm h-100 ${opacityClass}">
            <img src="${item.image}" class="card-img-top" alt="${item.name}" style="height: 200px; object-fit: cover;">
            <div class="card-body d-flex flex-column">
              <h5 class="card-title">${item.name}</h5>
              <p class="card-text mb-1"><strong>Order No.:</strong> ${order.orderNumber}</p>
              <p class="card-text mb-1"><strong>Payment:</strong> ${order.payment.status} - ${order.payment.cardHolder}</p>
              <p class="card-text mb-1"><strong>Amount Paid:</strong> $${item.price}</p>
              <p class="card-text mb-1"><strong>Quantity:</strong> ${item.quantity}</p>
              <p class="card-text mb-1"><strong>Status:</strong> ${statusText}</p>
              <p class="card-text mb-3"><strong>Warehouse:</strong> ${order.shipping.city}</p>
              <button class="btn btn-danger mt-auto" ${disabledBtn} onclick="returnOrder(${orderIndex}, ${itemIndex})">Return</button>
            </div>
          </div>
        </div>
      `;
      ordersContainer.innerHTML += orderHTML;
    });
  });
}

function returnOrder(orderIndex, itemIndex) {
  let orderData = JSON.parse(localStorage.getItem("orderDetails")) || [];
  const item = orderData[orderIndex].items[itemIndex];

  const confirmReturn = confirm(
    `Are you sure you want to return "${item.name}"?`
  );

  if (confirmReturn) {
    item.returned = true;
    localStorage.setItem("orderDetails", JSON.stringify(orderData));
    showAlert(`"${item.name}" has been returned to our warehouse.`);
    renderOrders();
  } else {
    showAlert(`"${item.name}" was not returned.`);
  }
}

renderOrders();

function logout() {
  localStorage.removeItem("userCredentials");
  alert("You have been logged out.");
  window.location.href = "/";
}

// *********************************************************

const userCredentials = JSON.parse(localStorage.getItem("userCredentials"));

if (userCredentials) {
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
