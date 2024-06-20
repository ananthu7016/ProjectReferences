// this if the Js for the create form
var today = new Date().toISOString().split('T')[0];
document.getElementById('Dob').setAttribute("max", today);
document.getElementById('CreateBtn').addEventListener("click", function (event) {

    var CreateForm = this.form;
    var isValid = true;
    event.preventDefault();

    //-----------------------------------------------------
    // then we need to get the error span of all the elements 
    let managerIdError = document.getElementById('ManagerIdSpan');
    let firstNameError = document.getElementById('FirstNameSpan');
    let lastNameError = document.getElementById('LastNameSpan');
    let deptNoError = document.getElementById('DeptNoSpan');
    let salaryError = document.getElementById('SalarySpan');
    let joiningDateError = document.getElementById('JoiningDateSpan');
    let dobError = document.getElementById('DobSpan');
    let genderError = document.getElementById('GenderSpan');

    //------------------------------------------------------
    // then we need some fields to store the value enterd by the user
    let managerId = document.querySelector("input[name='ManagerId']");
    let firstName = document.querySelector("input[name='FirstName']");
    let lastName = document.querySelector("input[name='LastName']");
    let deptNo = document.getElementById('DeptNo');
    let salary = document.querySelector("input[name='Salary']");
    let joiningDate = document.querySelector("input[name='JoiningDate']");
    let dob = document.querySelector("input[name='Dob']");
    let gender = document.querySelector("input[name='Gender']");
    //------------------------------------------------------


    //-------------------------------------------------------
    // we need to store the regularexpression needed to check the validations here
    const valManagerId = /^[0-9]{1}$/;
    const valFirstname = /^[^-\s][a-zA-Z.\s-]+$/;
    const valLastName = /^[^-\s][a-zA-Z.\s-]+$/;
    const valsalary = /^[0-9]{4,6}$/;
  



    // then we need to start the validation for each fields Values

    //-----------------------------------------------------------

    //ManagerID
    if (!managerId.value || managerId.value.startsWith(' ') || !valManagerId.test(managerId.value)) {
        // if control enter this block we need to show error
        managerIdError.textContent = "Not a Valid Id";
        isValid = false;
    }
    else {
        // if control enter this bock validation is perfect 
        managerIdError.textContent = "";
    }


    //------------------------------------------------------------
    //FirstName 
    if (!firstName.value || firstName.value.startsWith(' ') || !valFirstname.test(firstName.value)) {
        // the control enters this block we need to show the error message 
        firstNameError.textContent = "Not a Valid First Name";
        isValid = false;
    }
    else {
        firstNameError.textContent = "";
    }

    //--------------------------------------------------------------
    //LastName
    if (!lastName.value || lastName.value.startsWith(' ') || !valLastName.test(lastName.value)) {
        // the control enters this block we need to show the error message 
        lastNameError.textContent = "Not a Valid Last Name";
        isValid = false;
    }
    else {
        lastNameError.textContent = "";
    }


    //--------------------------------------------------------------
    //DeptNo 
    if (deptNo.value == 0 || deptNo.value=='0') {
        // then we need to show error message 
        deptNoError.textContent = "Choose a Department";
        isValid = false
    }
    else {
        deptNoError.textContent = "";
    }


    //-------------------------------------------------------------
    //for Salary 
    if (!salary.value || salary.value.startsWith(' ') || !valsalary.test(salary.value)) {
        // the control enters this block we need to show error message
        salaryError.textContent = "Not a Valid Salary";
        isValid = false;
    }
    else {
        salaryError.textContent = "";
    }

    //------------------------------------------------------------------
    //for Gender 
    var maleCheckbox = document.getElementById("malecheckbox");
    var femaleCheckbox = document.getElementById("femalecheckbox");


    var genderErrorSpan = document.getElementById("genderError");


    if (!maleCheckbox.checked && !femaleCheckbox.checked) {
        // If none is checked, display error message
        genderError.textContent = "Please select a gender";
        isValid = false;
    } else {
        // If at least one is checked, clear error message
        genderError.textContent = "";
        isValid = true;
    }

    //-------------------------------------------------------------------
    // for Joinig Date 
    if (!joiningDate.value) {
        // the control enteres here if the joining date is not entered 
        joiningDateError.textContent = "Choose Joining Date";
        isValid = false;
    }
    else {
        joiningDateError.textContent = "";
        
    }

    //--------------------------------------------------------------------
    // for date of birth 
    if (!dob.value) {
        // control enters here if dateofbirth is not entred
        dobError.textContent = "Choose Date of Birth";
        isValid = false;
    }
    else {
        dobError.textContent = "";
    }


    // so if every validation is true only the form will be submitted 
    if (isValid)
    {
        CreateForm.submit();
    }

    

    


})