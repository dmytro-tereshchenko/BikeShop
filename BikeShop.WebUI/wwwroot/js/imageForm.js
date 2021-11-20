function addImageBar() {
    this.parentNode.children[3].innerText = this.files[0].name;
    this.parentNode.children[4].value = this.files[0].name;
    this.parentNode.children[0].src = URL.createObjectURL(this.files[0]);
    this.parentNode.children[0].style.display = "inline-block";
}
document.querySelectorAll("input[id*='file']").forEach(element => element.addEventListener("change", addImageBar));
document.getElementById("addImage").addEventListener("click", function (e) {
    e.preventDefault();
    let node = this.parentNode.querySelector("div:last-of-type").cloneNode(true);
    let id = node.children[1].getAttribute("for");
    let num = id.substring(4);
    num = Number.parseInt(num) + 1;
    id = id.substring(0, 4) + num;
    node.children[1].setAttribute("for", id);
    node.children[2].id = id;
    node.children[2].files = undefined;
    node.children[3].innerText = "";
    node.children[4].value = "";
    node.children[0].src = "#";
    node.children[0].style.display = "none";
    node.children[2].addEventListener("change", addImageBar);

    this.parentNode.insertBefore(node, this);
})