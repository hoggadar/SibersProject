export interface LoginDto {
  email: string;
  password: string;
}

export interface SignupDto {
  firstName: string;
  lastName: string;
  patronymic: string;
  email: string;
  password: string;
}

export interface TokenDto {
  token: string;
}