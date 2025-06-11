let wishlist = JSON.parse(localStorage.getItem("wishlist")) || [];
let cart = JSON.parse(localStorage.getItem("cart")) || [];

// Fetch furniture items from API and populate wishlist
async function fetchWishlist() {
  try {
    const response = await fetch(
      "https://furnistyle.runasp.net/api/Furniture/CreateFurniture"
    );
    const data = await response.json();

    if (wishlist.length === 0) {
      wishlist = data.map((item) => ({
        id: item.id,
        name: item.name,
        price: item.price,
        image: item.image || "default.jpg",
      }));
      localStorage.setItem("wishlist", JSON.stringify(wishlist));
    }
    renderWishlist();
  } catch (error) {
    console.error("Error fetching wishlist data:", error);
  }
}

function renderWishlist() {
  const wishlistContainer = document.getElementById("wishlist-items");
  wishlistContainer.innerHTML = "";

  wishlist.forEach((item, index) => {
    wishlistContainer.innerHTML += `
            <div class="wishlist-item">
                <img src="${item.image}" alt="${item.name}">
                <span>${item.name}</span>
                <span>$${item.price}</span>
                <button class="add-to-cart" onclick="addToCart(${index})">Add to Cart</button>
                <button class="remove-btn" onclick="removeFromWishlist(${index})">X</button>
            </div>`;
  });

  localStorage.setItem("wishlist", JSON.stringify(wishlist));
}

function addToCart(index) {
  let product = { ...wishlist[index] };

  // تأكد من أن السعر رقم وليس string
  if (typeof product.price === "string") {
    product.price = parseFloat(product.price);
  }

  // حدد كمية ابتدائية للمنتج
  if (!product.quantity || isNaN(product.quantity)) {
    product.quantity = 1;
  }

  cart.push(product);
  localStorage.setItem("cart", JSON.stringify(cart));
  removeFromWishlist(index);
}

function removeFromWishlist(index) {
  wishlist.splice(index, 1);
  renderWishlist();
}

function moveAllToCart() {
  let updatedWishlist = wishlist.map((product) => {
    let updatedProduct = { ...product };

    // تحويل السعر إلى رقم إذا كان string
    if (typeof updatedProduct.price === "string") {
      updatedProduct.price = parseFloat(updatedProduct.price);
    }

    // إضافة كمية ابتدائية إذا مش موجودة أو غير صالحة
    if (!updatedProduct.quantity || isNaN(updatedProduct.quantity)) {
      updatedProduct.quantity = 1;
    }

    return updatedProduct;
  });

  cart = cart.concat(updatedWishlist);
  wishlist = [];

  localStorage.setItem("cart", JSON.stringify(cart));
  localStorage.setItem("wishlist", JSON.stringify(wishlist));

  renderWishlist();
}

// Initialize wishlist
document.addEventListener("DOMContentLoaded", fetchWishlist);

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
