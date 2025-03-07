import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  isDarkTheme: boolean = false;

  constructor() {
    const theme = localStorage.getItem('theme');

    if (theme) {
      this.isDarkTheme = theme === 'dark';
    }
    this.applyTheme();
  }

  toggleTheme() {
    this.isDarkTheme = !this.isDarkTheme;
    this.applyTheme();

    localStorage.setItem('theme', this.isDarkTheme ? 'dark' : 'light');
  }

  isDarkMode() {
    return this.isDarkTheme;
  }

  private applyTheme() {
    if (this.isDarkTheme) {
      document.body.classList.add('dark-theme');
    } else {
      document.body.classList.remove('dark-theme');
    }
  }
}
