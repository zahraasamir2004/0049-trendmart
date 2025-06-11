const nameInput = document.getElementById("nameInput");
const emailInput = document.getElementById("emailInput");
const mobileInput = document.getElementById("mobileInput");
const messageInput = document.getElementById("messageInput");
const loginBtn = document.getElementById("loginBtn");
const emailError = document.getElementById("emailError");
const mobileError = document.getElementById("mobileError");
const nameError = document.getElementById("nameError");
const messageError = document.getElementById("messageError");

function validateForm() {
  const name = nameInput.value.trim();
  const email = emailInput.value.trim();
  const mobile = mobileInput.value.trim();
  const message = messageInput.value.trim();

  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  const phoneRegex = /^[0-9]{10,15}$/;
  const nameRegex = /^[a-zA-Z\s]{5,}$/;
  const messageRegex = /.{10,}/;

  let isValid = true;

  if (!nameRegex.test(name)) {
    nameError.style.display = "block";
    isValid = false;
  } else {
    nameError.style.display = "none";
  }

  if (!emailRegex.test(email)) {
    emailError.style.display = "block";
    isValid = false;
  } else {
    emailError.style.display = "none";
  }

  if (!phoneRegex.test(mobile)) {
    mobileError.style.display = "block";
    isValid = false;
  } else {
    mobileError.style.display = "none";
  }

  if (!messageRegex.test(message)) {
    messageError.style.display = "block";
    isValid = false;
  } else {
    messageError.style.display = "none";
  }

  loginBtn.disabled = !isValid;
  loginBtn.style.opacity = isValid ? "1" : "0.3";
  loginBtn.style.cursor = isValid ? "pointer" : "not-allowed";
}

nameInput.addEventListener("input", validateForm);
emailInput.addEventListener("input", validateForm);
mobileInput.addEventListener("input", validateForm);
messageInput.addEventListener("input", validateForm);

loginBtn.addEventListener("click", function () {
  if (loginBtn.disabled) return;

  loginBtn.disabled = true;
  btnText.style.display = "none";
  loadingSpinner.style.display = "inline";

  setTimeout(() => {
    const userEmailValue = emailInput.value.trim();
    userEmail.textContent = userEmailValue;
    mainSection.style.display = "none";
    successMessage.style.display = "block";
  }, 1500);
});

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
