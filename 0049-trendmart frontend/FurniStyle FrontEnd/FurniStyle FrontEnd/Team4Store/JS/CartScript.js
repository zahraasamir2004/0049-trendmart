let cart = JSON.parse(localStorage.getItem("cart")) || [];

// Fetch furniture data from API and populate the cart
async function fetchFurniture() {
  try {
    const response = await fetch(
      "https://furnistyle.runasp.net/api/Furniture/CreateFurniture"
    );
    const data = await response.json();

    if (cart.length === 0) {
      cart = data.map((item) => ({
        id: item.id,
        name: item.name,
        price: item.price,
        quantity: 1,
        image: item.image || "default.jpg",
      }));
      localStorage.setItem("cart", JSON.stringify(cart));
    }
    renderCart();
  } catch (error) {
    console.error("Error fetching furniture data:", error);
  }
}

function renderCart() {
  const cartContainer = document.getElementById("cart-items");
  cartContainer.innerHTML = "";
  let total = 0;

  cart.forEach((item, index) => {
    let subtotal = item.price * item.quantity;
    total += subtotal;

    cartContainer.innerHTML += `
            <div class="cart-item">
                <img src="${item.image}" alt="${item.name}">
                <span>${item.name}</span>
                <input 
                    type="number" 
                    value="${item.quantity}" 
                    min="1" 
                    max="${item.stockQuantity}" 
                    class="quantity" 
                    data-index="${index}"
                    oninput="validateQuantity(this, ${item.stockQuantity})"
                >
                <span class="subtotal">$${subtotal}</span>
                <button class="remove-btn" onclick="removeItem(${index})">X</button>
            </div>`;
  });

  document.getElementById("total-price").innerText = total;
  localStorage.setItem("cart", JSON.stringify(cart));
  attachEventListeners();
}
function validateQuantity(input, maxStock) {
  let value = parseInt(input.value);

  if (isNaN(value) || value < 1) {
    input.value = 1;
  } else if (value > maxStock) {
    input.value = maxStock;
  }

  // تحديث الكمية في الـ cart
  const index = parseInt(input.dataset.index);
  cart[index].quantity = parseInt(input.value);

  renderCart(); // إعادة تحديث العرض والحساب
}

function removeItem(index) {
  cart.splice(index, 1);
  renderCart();
}

function updateQuantity(event) {
  let index = event.target.dataset.index;
  cart[index].quantity = parseInt(event.target.value);
  renderCart();
}

function attachEventListeners() {
  document.querySelectorAll(".quantity").forEach((input) => {
    input.addEventListener("change", updateQuantity);
  });
}

// Initialize
document.addEventListener("DOMContentLoaded", fetchFurniture);

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
