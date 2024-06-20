document.getElementById('btnSave').addEventListener("click", function (event) {

    var empForm = this.form;
    var isValid = true;
    event.preventDefault();


    // validate name and validate Designation 
    var name = document.querySelector("input[name='Name']");
    var nameError = document.getElementById("nameErrorSpan")

    

    // check the validation 
    if (!name.value || name.value.startsWith(' ')) {
        document.getElementById("Name").style.borderColor = "red";
        nameError.textContent = "Name is Required and Should not start with space"
        isValid = false;
        return;

    }
    else
    {
        nameError.textContent = "";
    }



    // then we need to validate the Designation 
    var designation = document.querySelector("input[Name='Designation']")
    var designationerror = document.getElementById('DesignationErrorSpan');


    // checking validation 
    if (!designation.value || designation.value.startsWith(' ')) {
        document.getElementById("Designation").style.borderColor = "red";
        designationerror.textContent = "Designation cannot have this Value";
        isValid = false;
        return;
    }
    else
    {
        designationerror.textContent = "";
    }


    if (isValid)
    {
        empForm.submit();
    }

});