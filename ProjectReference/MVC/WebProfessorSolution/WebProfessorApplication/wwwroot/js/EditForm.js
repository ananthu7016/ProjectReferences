document.getElementById('EditBtn').addEventListener("click", function (event) {


    event.preventDefault();
    alert("Control Entered");
    let isValid = true;
    var EditForm = this.form;
    let salary = document.querySelector("input[name = 'Salary']");
    let salaryError = document.getElementById('Salaryspan');
    const valsalary = /^[0-9]{4,6}$/;
    //for Salary
    if (!salary.value || salary.value.startsWith(' ') || !valsalary.test(salary.value)) {
        // the control enters this block we need to show error message
        salaryError.textContent = "Not a Valid Salary";
        isValid = false;
        return;
    }
    else {
        salaryError.textContent = "";
    }


    if (isValid) {
        EditForm.submit();
        alert(isValid);
    }

})