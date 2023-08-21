import { AbstractControl, ValidationErrors, Validators } from "@angular/forms";

export const passwordValidator = function (control: AbstractControl): ValidationErrors | null {
    const value: string = control.value || '';

    if (value.length < 8) {
        return { 
            passwordStrength: `At least 8 characters` 
        }
    }
  
    const upperCaseCharacters = /[A-Z]+/g
    if (!upperCaseCharacters.test(value)) {
        return { 
            passwordStrength: `Upper case required` 
        };
    }
  
    const lowerCaseCharacters = /[a-z]+/g
    if (!lowerCaseCharacters.test(value)) {
        return { 
            passwordStrength: `lower case required` 
        };
    }
  
  
    const numberCharacters = /[0-9]+/g
    if (!numberCharacters.test(value)) {
        return { 
            passwordStrength: `number required` 
        };
    }
  
    const specialCharacters = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/
    if (!specialCharacters.test(value)) {
        return { 
            passwordStrength: `special char required` 
        };
    }
     
    return null;
}

export const emailValidator = function (control: AbstractControl): ValidationErrors | null {
    const value: string = control.value || '';
    if (value.length === 0) {
        return {
            email: "Email address is required"
        }
    }

    const errors = Validators.email(control);
    if(errors && errors['email']) {
        return {
            email: "Invalid email address"
        }
    }

    return errors;
}