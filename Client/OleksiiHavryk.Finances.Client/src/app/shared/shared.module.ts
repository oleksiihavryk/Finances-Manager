import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputComponent } from './input/input.component';
import { ModalComponent } from './modal/modal.component';
import { ReactiveFormsModule } from '@angular/forms';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';



@NgModule({ declarations: [
        InputComponent,
        ModalComponent,
    ],
    exports: [
        InputComponent,
        ModalComponent
    ], imports: [CommonModule,
        ReactiveFormsModule], providers: [provideHttpClient(withInterceptorsFromDi())] })
export class SharedModule { }
