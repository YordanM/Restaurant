import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { JwtModule } from "@auth0/angular-jwt";
import { tokenGetter } from "./app.module";
import { AuthGuard } from "./auth/auth.guard";
import { AuthAdminGuard } from "./auth/authAdmin.guard";
import { CategoriesComponent } from "./categories/categories.component";
import { CategoryEditComponent } from "./categories/category-edit/category-edit.component";
import { CategoryNewComponent } from "./categories/category-new/category-new.component";
import { LoginComponent } from "./login/login.component";
import { ProductEditComponent } from "./products/product-edit/product-edit.component";
import { ProductNewComponent } from "./products/product-new/product-new.component";
import { ProductsComponent } from "./products/products.component";
import { UserCurrentEditComponent } from "./users/user-current-edit/user-current-edit.component";
import { UserEditComponent } from "./users/user-edit/user-edit.component";
import { UserNewComponent } from "./users/user-new/user-new.component";
import { UsersComponent } from "./users/users.component";

const appRoutes: Routes = [
    { path: '', redirectTo: '/login', pathMatch: 'full'},
    { path: 'products', component: ProductsComponent, canActivate: [AuthAdminGuard] },
    { path: 'products/new', component: ProductNewComponent, canActivate: [AuthAdminGuard] },
    { path: 'products/edit/:id', component: ProductEditComponent, canActivate: [AuthAdminGuard] },
    { path: 'categories', component: CategoriesComponent, canActivate: [AuthAdminGuard] },
    { path: 'categories/new', component: CategoryNewComponent, canActivate: [AuthAdminGuard] },
    { path: 'categories/edit/:id', component: CategoryEditComponent, canActivate: [AuthAdminGuard] },
    { path: 'users', component: UsersComponent, canActivate: [AuthAdminGuard] },
    { path: 'users/new', component: UserNewComponent, canActivate: [AuthAdminGuard]},
    { path: 'users/edit', component: UserCurrentEditComponent, canActivate: [AuthGuard]},
    { path: 'users/edit/:userId', component: UserEditComponent, canActivate: [AuthAdminGuard]},
    { path: 'login', component: LoginComponent }
]

@NgModule({
    imports: [RouterModule.forRoot(appRoutes), JwtModule.forRoot({
        config: {
            tokenGetter: tokenGetter,
            allowedDomains: ['localhost:44351'],
            disallowedRoutes: [],
        }
    })],
    exports: [RouterModule],
})
export class AppRoutingModule{

}