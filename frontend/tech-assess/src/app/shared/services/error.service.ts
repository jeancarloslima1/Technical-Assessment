import { Injectable } from '@angular/core';
import { ErrorCode } from '../interfaces/error-code.enum';

const errorMessages: Record<number, string> = {
  [ErrorCode.ValidationError]: 'Invalid input data. Please check the details and try again',
  [ErrorCode.NotFound]: 'The requested resource could not be found',
  [ErrorCode.ResourceNotCreated]: 'Failed to create the resource. Please try again',
  [ErrorCode.InvalidId]: 'The requested resource is invalid',
  [ErrorCode.UpdateFailed]: 'The update operation failed. Please try again',
  [ErrorCode.DeleteFailed]: 'The resource could not be deleted. Please try again',
  [ErrorCode.InternalServerError]: 'An internal server error occurred. Please try again later',
  [ErrorCode.Unauthorized]: 'Your session has expired. Please log in again to continue.',
};

@Injectable({
  providedIn: 'root'
})
export class ErrorService {
  constructor() { }

  getErrorMessage(errorCode: ErrorCode): string {
    return errorMessages[errorCode] || 'An unknown error occurred.';
  }
}
