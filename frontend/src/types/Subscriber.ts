export interface Subscriber {
  id: number;
  name: string;
  email: string;
  createdAt: string;
}

export interface NewSubscriber {
  name: string;
  email: string;
}
