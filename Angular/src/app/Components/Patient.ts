export interface Patient {
    patientID: number; // Optional property for ID
    firstName: string;
    lastName: string;
    dateOfBirth: string; // Use Date type to handle date values
    gender: string;
    phoneNumber: string;
    email: string;
    address: string;
  }