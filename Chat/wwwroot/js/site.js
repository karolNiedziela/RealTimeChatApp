const menuBtn = document.querySelector(".menu_btn");
const sidebar = document.querySelector(".sidebar");

menuBtn.addEventListener("click", () => {
  sidebar.classList.toggle("active");
  menuBtnChange();
});

function menuBtnChange() {
  if (sidebar.classList.contains("active")) {
    menuBtn.classList.replace("bx-menu", "bx-exit");
  } else {
    menuBtn.classList.replace("bx-exit", "bx-menu");
  }
}

//const modal = document.querySelector(".modal");
//function showModal() {
//  modal.classList.add("active");
//}

//function closeModal() {
//  modal.classList.remove("active");
//}

$(function () {
  $(".nav-list-li").hover(
    function () {
      $(".sidebar-user.show").css("z-index", "-1");
    },
    function () {
      $(".sidebar-user.show").css("z-index", "0");
    }
  );
});

document.querySelectorAll(".my-tooltip").forEach((tooltip) => {
  const lengthOfTextContext = tooltip.textContent.length;
  switch (true) {
    case lengthOfTextContext < 10:
      tooltip.style.marginLeft = "10px";
      break;

    case lengthOfTextContext >= 10 && lengthOfTextContext < 20:
      tooltip.style.marginLeft = "25px";
      break;

    case lengthOfTextContext >= 20 && lengthOfTextContext < 25:
      tooltip.style.marginLeft = "40px";
      break;

    case lengthOfTextContext >= 25 && lengthOfTextContext <= 30:
      tooltip.style.marginLeft = "60px";
      break;
  }
});
