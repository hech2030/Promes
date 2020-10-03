export class User {
  constructor() {
    this.id = '';
  }
  id: string ='';
  username: string;
  phoneNumber: string;
  accessFailedCount: number;
  email: string;
  lockoutEnabled: string;
  normalizedEmail: string;
  normalizedUserName: string;
  roleLabel: string;
  role: number;
  userName: string;
}
