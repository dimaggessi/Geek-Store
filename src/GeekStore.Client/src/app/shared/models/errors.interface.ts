export interface ErrorResponseInterface {
  error: ErrorInterface;
  validationErrors?: ValidationErrorInterface[] | null;
}

export interface ErrorInterface {
  code: string;
  message: string;
}

export interface ValidationErrorInterface {
  code: string;
  message: string;
}
