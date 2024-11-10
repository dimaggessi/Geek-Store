import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { RouterLink } from "@angular/router";

@Component({
    standalone: true,
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrl: './register.component.scss',
    imports: [ReactiveFormsModule]
})
export class RegisterComponent implements OnInit {
    
    form!: FormGroup;

    constructor(private fb: FormBuilder){}

    ngOnInit(): void {
        this.form = this.fb.nonNullable.group({
            firstName: ['', Validators.required],
            lastName: ['', Validators.required],
            email: ['', Validators.required],
            password: ['', Validators.required]
        });
    }

    onSubmit(){
        console.log('form', this.form.getRawValue());
    }
}