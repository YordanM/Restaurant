import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Byte } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';
import { User } from '../user.model';

export interface EditCurrentUserResponseData {
  message: string;
}

@Component({
  selector: 'app-user-current-edit',
  templateUrl: './user-current-edit.component.html',
  styleUrls: ['./user-current-edit.component.css']
})
export class UserCurrentEditComponent implements OnInit {
  error: string = '';
  editCurrentUserForm: FormGroup;
  bytes: Byte[];
  picture: any;
  name: string;
  role: string;
  email: string = 'test';
  password: string;
  emailTaken: boolean = false;
  userHasImage: boolean = false;
  user: User;
  private userSub: Subscription;
  
  // error messages
  emailError = 'Please enter a valid email (less than 255 characters).';

  constructor(private authService: AuthService, private _http: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.initForm();
    this.authService.getCurrentUser().subscribe(user => {
      
    });
  }

  private initForm(){
    this.editCurrentUserForm = new FormGroup({
      'picture': new FormControl(null),
      'firstName': new FormControl(null, [
      ]),
      'lastName': new FormControl(null, [
      ]),
      'email': new FormControl(null, [
        Validators.email,
      ]),
      'password': new FormControl(null, [
      ])
    });
  }

  onSubmit(){
    let picture = this.editCurrentUserForm.get('picture').value;
    let firstName = this.editCurrentUserForm.get('firstName').value;
    let lastName = this.editCurrentUserForm.get('lastName').value;
    let email = this.editCurrentUserForm.get('email').value;
    let password = this.editCurrentUserForm.get('password').value;

    this.updateCurrentUser(firstName, lastName, email, password, picture).subscribe(response => {
        this.emailTaken = false;
        this.router.navigate(['/users']);

      const userData: {
        email: string;
        name: string;
        role: string;
        _token: string;
        _tokenExpirationDate: string;
      } = JSON.parse(localStorage.getItem('userData'));

      let fName = '';
      let lName = '';
      let mail = '' ? userData.email : email;
      let name = '';

      if(firstName != null || lastName != null){
        fName = firstName === null ? userData.name.split(' ')[0] : firstName;
        lName = lastName === null ? userData.name.split(' ')[1] : lastName;
      }

      name = fName + ' ' + lName;

      userData['email'] = mail;
      userData.name = name;
      localStorage.setItem('userData', JSON.stringify(userData));
      console.log('success');
    },
    err => {
      
    });
    console.log();
  }

  private updateCurrentUser(firstName: string, lastName: string, email: string, password: string, picture: File){
    return this._http.put<EditCurrentUserResponseData>("https://localhost:44351/api/User/me", {
      firstName,
      lastName,
      email,
      password,
      picture,
    },
    {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

}
