const endDate = new Date("April 15, 2025 00:00:00 PDT").getTime();
const countdown = setInterval(() => {
  const now = new Date().getTime();
  const timeLeft = endDate - now;
  const days = Math.floor(timeLeft / (1000 * 60 * 60 * 24));
  const hours = Math.floor(
    (timeLeft % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60)
  );
  const minutes = Math.floor((timeLeft % (1000 * 60 * 60)) / (1000 * 60));
  const seconds = Math.floor((timeLeft % (1000 * 60)) / 1000);
  document.getElementById("days").innerText = days < 10 ? "0" + days : days;
  document.getElementById("hours").innerText = hours < 10 ? "0" + hours : hours;
  document.getElementById("minutes").innerText =
    minutes < 10 ? "0" + minutes : minutes;
  document.getElementById("seconds").innerText =
    seconds < 10 ? "0" + seconds : seconds;

  if (timeLeft < 0) {
    clearInterval(countdown);
    document.querySelector(".countdown-box").innerHTML =
      "<h3> opps flash sale ended !</h3>";
  }
}, 1000);
// ************************************
var typed = new Typed(".typing ", {
  strings: [
    "for the fastest",
    "for the strongest",
    "for the newest",
    "for the best",
  ],
  typeSpeed: 100,
  backSpeed: 180,
  loop: true,
});
// ***********************************************
document.addEventListener("DOMContentLoaded", function () {
  function addToCart(item) {
    let cart = JSON.parse(localStorage.getItem("cart")) || [];
    let existingItem = cart.find((i) => i.id === item.id);

    if (existingItem) {
      existingItem.quantity += 1;
    } else {
      item.quantity = 1;
      cart.push(item);
    }
    localStorage.setItem("cart", JSON.stringify(cart));
    alert("item added to cart in localStorage only not to API endpoint");
    updateCounts();
  }

  function addToWishlist(item) {
    let wishlist = JSON.parse(localStorage.getItem("wishlist")) || [];
    let exists = wishlist.some((i) => i.id === item.id);

    if (!exists) {
      wishlist.push(item);
      localStorage.setItem("wishlist", JSON.stringify(wishlist));
      alert("item added to wishlist!");
      updateCounts();
    } else {
      alert(
        "item is already in wishlist , you can not add the same item twice !"
      );
    }
  }

  function updateCounts() {
    let cart = JSON.parse(localStorage.getItem("cart")) || [];
    let wishlist = JSON.parse(localStorage.getItem("wishlist")) || [];

    document.getElementById("cartCount").textContent = cart.length;
    document.getElementById("wishlistCount").textContent = wishlist.length;
  }

  // Hussein Adel APIs Docs
  // https://documenter.getpostman.com/view/41946833/2sB2cRDQVc#711d54a3-939f-40f7-ab34-f2383db20948

  fetch("https://furnistyle.runasp.net/api/Furniture/AllFurniture")
    .then((response) => response.json())
    .then((products) => {
      const carousels = document.querySelectorAll(".owl-carousel");
      let categories = {};

      // Group products by category
      products.forEach((product) => {
        if (!categories[product.categoryName]) {
          categories[product.categoryName] = [];
        }
        categories[product.categoryName].push(product);
      });
      carousels.forEach((carousel, index) => {
        const categoryName = carousel.getAttribute("data-category");
        if (categories[categoryName]) {
          categories[categoryName].forEach((product) => {
            const productHTML = `
              <div class="item" data-id="${product.id}" data-name="${
              product.name
            }" data-price="${product.price}" data-image="${product.pictureUrl}">
                <div class="px-0 image-container" style="position: relative; max-width: 250px; margin: auto; text-align: center;">
                  <div style="width: 185px; height: 200px; display: flex; align-items: center; justify-content: center; overflow: hidden; margin: auto;">
                    <img src="${
                      product.pictureUrl
                    }" style="width: 100%; height: 100%; object-fit: cover;" />
                  </div>
                  <div class="discount-bar">-30%</div>
                  <div>
                    <div class="heart-icon" style="cursor: pointer">
                      <i class="fa-regular fa-heart add-to-wishlist"></i>
                    </div>
                    <div class="eye-icon">
                      <a href="ProductDetails.html?id=${product.id}">
                        <i class="fa-regular fa-eye"></i>
                      </a>
                    </div>
                  </div>
                  <a href="#" class="btnaddtocart">Add to Cart</a>
                </div>

                <div style="padding: 10px; width: 100%; padding-top: 40px">
                  <h6>${product.name}</h6>
                  <div class="d-flex gap-2 justify-content-start mt-2">
                    <h6 class="text-danger">$${product.price}</h6>
                    <h6 class="original-price" style="font-size: 13px; padding-top: 2px">
                      $${(product.price * 1.3).toFixed(2)}
                    </h6>
                  </div>
                  <div>
                    <span class="fa fa-star checkedrating"></span>
                    <span class="fa fa-star checkedrating"></span>
                    <span class="fa fa-star checkedrating"></span>
                    <span class="fa fa-star"></span>
                    <span class="fa fa-star"></span>
                    <span class="text-secondary"> (86)</span>
                  </div>
                </div>
              </div>
            `;
            carousel.innerHTML += productHTML;
          });

          $(carousel).owlCarousel({
            items: 4,
            loop: true,
            margin: 20,
            autoplay: true,
            autoplayTimeout: 900,
            autoplayHoverPause: false,
            responsiveClass: true,
            responsive: {
              0: { items: 2, nav: true, loop: true, margin: 10 },
              600: { items: 4, nav: false, loop: true, margin: 15 },
              1000: { items: 7, nav: true, loop: true, margin: 20 },
            },
          });

          carousel.querySelectorAll(".item").forEach((item) => {
            item.addEventListener("mouseenter", function () {
              $(carousel).trigger("stop.owl.autoplay");
            });

            item.addEventListener("mouseleave", function () {
              $(carousel).trigger("play.owl.autoplay", [1000]);
            });
          });
        }
      });

      document.querySelectorAll(".item").forEach((itemElement) => {
        let item = {
          id: itemElement.getAttribute("data-id"),
          name: itemElement.getAttribute("data-name"),
          price: itemElement.getAttribute("data-price"),
          image: itemElement.getAttribute("data-image"),
        };

        itemElement
          .querySelector(".btnaddtocart")
          .addEventListener("click", (e) => {
            e.preventDefault();
            addToCart(item);
          });

        itemElement
          .querySelector(".add-to-wishlist")
          .addEventListener("click", (e) => {
            e.preventDefault();
            addToWishlist(item);
          });
      });

      updateCounts();
    })
    .catch((error) => {
      console.error("error fetching products", error);
    });
});

function logout() {
  localStorage.removeItem("userCredentials");
  alert("You have been logged out.");
  window.location.href = "/";
}

// *************************************
const footerText = document.getElementById("footerText");
const currentYear = new Date().getFullYear();
footerText.innerHTML = `&copy; ${currentYear} All rights reserved Team 4 Store`;

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
