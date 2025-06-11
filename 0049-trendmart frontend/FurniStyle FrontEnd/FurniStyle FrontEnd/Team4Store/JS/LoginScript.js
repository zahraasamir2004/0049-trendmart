const emailInput = document.getElementById("emailInput");
const passwordInput = document.getElementById("passwordInput");
const loginBtn = document.getElementById("loginBtn");
const mainSection = document.getElementById("mainSection");
const successMessage = document.getElementById("successMessage");
const btnText = document.getElementById("btnText");
const loadingSpinner = document.getElementById("loadingSpinner");
const emailError = document.getElementById("emailError");
const passwordError = document.getElementById("passwordError");
const userEmail = document.getElementById("userEmail");

window.onload = function () {
  const urlParams = new URLSearchParams(window.location.search);
  const email = urlParams.get("email");
  if (email) {
    emailInput.value = decodeURIComponent(email);
    validateForm();
  }
};

function validateForm() {
  const email = emailInput.value.trim();
  const password = passwordInput.value.trim();
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  const phoneRegex = /^[0-9]{10,15}$/;
  let isValid = true;

  if (!email) {
    emailError.textContent = "Email or phone number is required";
    emailError.style.display = "block";
    isValid = false;
  } else if (!emailRegex.test(email) && !phoneRegex.test(email)) {
    emailError.textContent = "Invalid email or phone number";
    emailError.style.display = "block";
    isValid = false;
  } else {
    emailError.style.display = "none";
  }

  if (!password) {
    passwordError.textContent = "Password is required";
    passwordError.style.display = "block";
    isValid = false;
  } else if (password.length < 6) {
    passwordError.textContent = "Password must be at least 6 characters";
    passwordError.style.display = "block";
    isValid = false;
  } else {
    passwordError.style.display = "none";
  }

  

  loginBtn.disabled = !isValid;
  loginBtn.style.opacity = isValid ? "1" : "0.3";
  loginBtn.style.cursor = isValid ? "pointer" : "not-allowed";


  return isValid;
}

emailInput.addEventListener("input", validateForm);
passwordInput.addEventListener("input", validateForm);

async function loginUser() {
  if (!validateForm()) return;

  loginBtn.disabled  = true;
  btnText.style.display = "none";
  loadingSpinner.style.display = "inline-block";

  const loginData = {
    email: emailInput.value.trim(),
    password: passwordInput.value.trim(),
  };

  try {
    const response = await fetch(
      "https://furnistyle.runasp.net/api/Account/Login",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
        },
        body: JSON.stringify(loginData),
      }
    );
    const data = await response.json();
    console.log("Login response:", data);

    if (response.ok) {
      localStorage.setItem(
        "userCredentials",
        JSON.stringify({
          id: data.id,
          email: data.email,
          token: data.token,
          name: data.displayName,
          role: data.role,
          phoneNumber: data.phoneNumber,
        })
      );
      userEmail.textContent = data.email;
      document.getElementById("userRole").textContent = data.role;
      mainSection.style.display = "none";
      successMessage.style.display = "block";

      setTimeout(() => {
        if (data.role === "Admin") {
          window.location.href = "./Dashboard/index.html";
        } else {
          window.location.href = "/Index.html";
        }
      }, 2000);
    } else {
      throw new Error(data.message || "Login failed");
    }
  } catch (error) {
    console.error("Error:", error);
    alert(error.message || "An error occurred. Please try again.");
  } finally {
    loginBtn.disabled  = false;
    btnText.style.display = "inline";
    loadingSpinner.style.display = "none";
  }
}
loginBtn.addEventListener("click", loginUser);
document.querySelectorAll("#signInBtn").forEach((button) => {
  button.addEventListener("click", function () {
    document.getElementById("message").style.display = "block";
  });
});
function checkLoginStatus() {
  const credentials = localStorage.getItem("userCredentials");
  if (credentials) {
    const { email, role } = JSON.parse(credentials);
    userEmail.textContent = email;
    document.getElementById("userRole").textContent = role;
    mainSection.style.display = "none";
    successMessage.style.display = "block";

    setTimeout(() => {
      if (role === "Admin") {
        window.location.href = "./Dashboard/index.html";
      } else {
        window.location.href = "/Index.html";
      }
    }, 4000);
  }
}
checkLoginStatus();

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
