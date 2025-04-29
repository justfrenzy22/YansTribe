import { IHeaders } from "@/types/IHeaders";
import { IRequestOptions } from "@/types/IRequestOptions";
import Cookie from "js-cookie";

export class ApiService {
	private baseUrl: string;

	constructor() {
		this.baseUrl = process.env.NEXT_PUBLIC_CSHARP_API_BASE_URL || ""; // Updated variable name
		if (!this.baseUrl) {
			console.error(
				"ApiService Error: NEXT_PUBLIC_CSHARP_API_BASE_URL environment variable is not set."
			);
			throw new Error(
				"ApiService Error: NEXT_PUBLIC_CSHARP_API_BASE_URL environment variable is not set."
			);
		}
	}

	public async request<T>({
		endpoint,
		method,
		options,
	}: IRequestOptions): Promise<T> {
		if (!this.baseUrl && !process.env.NEXT_PUBLIC_CSHARP_API_BASE_URL) {
			throw new Error("API Service is not configured. Base URL missing.");
		}
		const baseUrl = this.baseUrl || process.env.NEXT_PUBLIC_CSHARP_API_BASE_URL;
		const url = `${baseUrl}${endpoint}`;
		const headers = this.buildHeaders(options);
		const opt = this.buildRequest(method, headers, options);
		console.log(`Request URL: ${url}`);
		console.log(`Request Headers:`, headers);
		console.log(`Request Options:`, opt);

		try {
			const res = await fetch(url, opt);
			this.handleToken(res);

			const data = await this.handleResponse<T>(res, url);
			return data;
		} catch (networkErr: unknown) {
			console.error(`Network error during fetch to ${url}:`, networkErr);
			const errorMessage =
				networkErr instanceof Error
					? networkErr.message
					: "Network error occurred.";
			return {
				status: 500,
				message: errorMessage,
			} as unknown as T;
		}
	}

	private buildHeaders(options: IRequestOptions["options"]): IHeaders {
		const { files, form, requiresAuth, cookie } = options;

		const headers: Record<string, string> = {
			Accept: "application/json",
		};

		// Removed manual setting of Content-Type for multipart/form-data
		if (!form && !files) {
			headers["Content-Type"] = "application/json";
		}

		if (requiresAuth) {
			headers["auth_token"] = `Bearer ${cookie}`;
		}

		return headers;
	}

	private buildRequest(
		method: IRequestOptions["method"],
		headers: IHeaders,
		options: IRequestOptions["options"]
	) {
		const { body, form, requiresAuth, cookie } = options;

		if (requiresAuth) {
			headers["auth_token"] = `Bearer ${cookie}`;
		}

		const opt: RequestInit = {
			method: method,
			headers,
			credentials: requiresAuth ? "include" : "omit",
		};

		if (form) {
			console.log(`form`, form.get(`content`));
			opt.body = form;
		} else if (body) {
			opt.body = JSON.stringify(body);
		}

		return opt;
	}

	private handleToken(res: Response): void {
		console.log(`handle token headers`, res.headers);
		const auth_token = res.headers.get("auth_token");
		console.log(`handle token auth_token`, auth_token);

		if (auth_token) {
			const expirationDate = new Date(Date.now() + 1000 * 60 * 60 * 24);
			Cookie.set(`auth_token`, auth_token, {
				expires: expirationDate,
				crossSite: true,
				secure: true,
				path: "/",
			});
		} else {
			console.warn("auth_token header not found in the response.");
		}
	}

	private async handleResponse<T>(res: Response, url: string): Promise<T> {
		try {
			const data = await res.json();
			console.log(`handleResponse data:`, data);
			return data as unknown as T;
		} catch {
			return {
				status: 500,
				message: `Invalid JSON received from server for ${url}.`,
			} as unknown as T;
		}
	}
}
