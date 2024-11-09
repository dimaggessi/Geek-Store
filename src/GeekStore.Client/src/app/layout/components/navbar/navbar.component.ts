import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
    standalone: true,
    selector: 'app-navbar',
    imports: [NgbCollapseModule, CommonModule],
    templateUrl: './navbar.component.html',
    styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
    isCollapsed = false;
}