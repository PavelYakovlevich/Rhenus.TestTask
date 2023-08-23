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

export const nameValidator = (length: number) => {
    const label = "Name";
    return [notEmpty(label), onlyCharacters(label), maxLength(label, length)];
}


export const lastNameValidator = (length: number) => {
    const label = "Last name";
    return [notEmpty(label), onlyCharacters(label), maxLength(label, length)];
}

export const birthdayValidator = function (control: AbstractControl): ValidationErrors | null {
    const value: string = control.value || '';

    var date = Date.parse(value);
    var now = Date.now();

    if(date > now) {
        return { 
            birthday: `Birthday value must be in past`
        };
    }

    return null;
};


const notEmpty = (label: string) => function (control: AbstractControl): ValidationErrors | null {
    const value: string = control.value?.trim() || '';

    if(value.length === 0) {
        return { 
            lastName: `${label} is empty`
        };
    }

    return null;
};

const onlyCharacters = (label: string) => function (control: AbstractControl): ValidationErrors | null {
    const value: string = control.value?.trim() || '';

    const onlyCharacters = /^[A-Za-z]+$/g;

    if (!onlyCharacters.test(value)) {
        return { 
            onlyCharacters: `${label} must consist of chars only` 
        };
    }

    return null;
};

const maxLength = (label: string, maxLength: number) => function (control: AbstractControl): ValidationErrors | null {
    const value: string = control.value?.trim() || '';

    if(value.length > maxLength) {
        return { 
            name: `${label} must be maximum ${maxLength} characters long`
        };
    }

    return null;
};