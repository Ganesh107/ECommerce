
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';
import { AuthenticationComponent } from './authentication.component';

describe('AuthenticationComponent', () => {
  let component: AuthenticationComponent;
  let fixture: ComponentFixture<AuthenticationComponent>;


  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AuthenticationComponent],
      imports: [FormsModule]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthenticationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should bind userName input', () => {
    const input = fixture.debugElement.query(By.css('input[type="text"]')).nativeElement;
    input.value = 'testuser';
    input.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    expect(component.userName).toBe('testuser');
  });

  it('should bind password input', () => {
    const input = fixture.debugElement.query(By.css('input[type="password"]')).nativeElement;
    input.value = 'password123';
    input.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    expect(component.password).toBe('password123');
  });

  it('should disable CONTINUE button when disableButton is true', () => {
    component.disableButton = true;
    fixture.detectChanges();
    const button = fixture.debugElement.query(By.css('button[type="submit"]')).nativeElement;
    expect(button.disabled).toBeTrue();
  });

  it('should call loginUser on CONTINUE button click', () => {
    spyOn(component, 'loginUser');
    component.disableButton = false;
    fixture.detectChanges();
    const button = fixture.debugElement.query(By.css('button[type="submit"]'));
    button.triggerEventHandler('click', null);
    expect(component.loginUser).toHaveBeenCalled();
  });

  it('should show error message when invalidUserName is true', () => {
    component.invalidUserName = true;
    fixture.detectChanges();
    const errorMsg = fixture.debugElement.query(By.css('span.text-red-600'));
    expect(errorMsg).toBeTruthy();
    expect(errorMsg.nativeElement.textContent).toContain('Invalid Email ID/Mobile number');
  });
});
