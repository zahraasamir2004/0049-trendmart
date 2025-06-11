const nameInput = document.getElementById("nameInput");
const emailInput = document.getElementById("emailInput");
const phoneInput = document.getElementById("phoneInput");
const passwordInput = document.getElementById("passwordInput");
const loginBtn = document.getElementById("loginBtn");
const btnText = document.getElementById("btnText");
const loadingSpinner = document.getElementById("loadingSpinner");
const successMessage = document.getElementById("successMessage");
const userEmailSpan = document.getElementById("userEmail");
const emailError = document.getElementById("emailError");
const nameError = document.getElementById("nameError");
const phoneError = document.getElementById("phoneError");
const passwordError = document.getElementById("passwordError");

const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
const nameRegex = /^[a-zA-Z\s]{5,}$/;
const phoneRegex = /^(01[0-9]{9}|[0-9]{10,15})$/;
const passwordRegex = /^(?=.*[A-Z])(?=.*\d).{8,}$/;

function validateForm() {
  const name = nameInput.value.trim();
  const email = emailInput.value.trim();
  const phone = phoneInput.value.trim();
  const password = passwordInput.value.trim();
  let isValid = true;

  if (!name) {
    nameError.textContent = "Name is required";
    nameError.style.display = "block";
    isValid = false;
  } else if (!nameRegex.test(name)) {
    nameError.textContent =
      "Name must be at least 5 letters, only letters and spaces";
    nameError.style.display = "block";
    isValid = false;
  } else {
    nameError.style.display = "none";
  }

  if (!email) {
    emailError.textContent = "Email is required";
    emailError.style.display = "block";
    isValid = false;
  } else if (!emailRegex.test(email)) {
    emailError.textContent = "Invalid email format";
    emailError.style.display = "block";
    isValid = false;
  } else {
    emailError.style.display = "none";
  }

  if (!phone) {
    phoneError.textContent = "Phone number is required";
    phoneError.style.display = "block";
    isValid = false;
  } else if (!phoneRegex.test(phone)) {
    phoneError.textContent = "Invalid phone number format";
    phoneError.style.display = "block";
    isValid = false;
  } else {
    phoneError.style.display = "none";
  }

  if (!password) {
    passwordError.textContent = "Password is required";
    passwordError.style.display = "block";
    isValid = false;
  } else if (!passwordRegex.test(password)) {
    passwordError.textContent =
      "Password must be 8+ characters with 1 uppercase and 1 number";
    passwordError.style.display = "block";
    isValid = false;
  } else {
    passwordError.style.display = "none";
  }

  console.log("Form valid:", isValid);

  loginBtn.disabled = !isValid;
  loginBtn.style.opacity = isValid ? "1" : "0.3";
  loginBtn.style.cursor = isValid ? "pointer" : "not-allowed";

  return isValid;
}

[nameInput, emailInput, phoneInput, passwordInput].forEach((input) => {
  input.addEventListener("input", validateForm);
});

async function registerUser() {
  if (!validateForm()) return;

  loginBtn.disabled = true;
  btnText.style.display = "none";
  loadingSpinner.style.display = "inline-block";

  const userData = {
    email: emailInput.value.trim(),
    displayName: nameInput.value.trim(),
    phoneNumber: phoneInput.value.trim(),
    password: passwordInput.value.trim(),
  };

  try {
    const response = await fetch(
      "https://furnistyle.runasp.net/api/Account/Register",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
        },
        body: JSON.stringify(userData),
      }
    );

    const data = await response.json();
    console.log("data", data);

    if (response.ok) {
      document.getElementById("mainSection").style.display = "none";
      userEmailSpan.textContent = userData.email;
      successMessage.style.display = "block";

      setTimeout(() => {
        window.location.href = `/Login.html?email=${userData.email}`;
      }, 4000);
    } else {
      throw new Error(data.message || "registration failed");
    }
  } catch (error) {
    console.error("Error:", error);
    alert(error.message || "an error occurred. please try again.");
  } finally {
    loginBtn.disabled = false;
    btnText.style.display = "inline";
    loadingSpinner.style.display = "none";
  }
}

loginBtn.addEventListener("click", registerUser);

// ********************************************
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
