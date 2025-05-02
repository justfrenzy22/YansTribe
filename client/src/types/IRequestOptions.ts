// import { ConvPostFormData } from "./ICreatePostFormData";

export interface IRequestOptions {
	endpoint: string;
	method: `GET` | `POST` | `PUT` | `DELETE`;
	options: {
		body?: unknown;
		form?: FormData;
		files?: boolean;
		requiresAuth: boolean;
		cookie?: string | null;
	};
}
