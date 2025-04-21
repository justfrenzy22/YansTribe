import Cookie from "js-cookie";

export class ApiService {
	private baseUrl: string;

	constructor() {
		this.baseUrl = process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL || "";
		if (!this.baseUrl) {
			console.error(
				"ApiService Error: NEXT_PUBLIC_CSHARP_API_BASE_URL environment variable is not set."
			);
			throw new Error(
				"ApiService Error: NEXT_PUBLIC_CSHARP_API_BASE_URL environment variable is not set."
			);
		}
	}

	public async request<T>(
		endpoint: string,
		method: string = "GET",
		body: any = null,
		requiresAuth: boolean = false
	): Promise<T> {
		if (!this.baseUrl && !process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL) {
			throw new Error("API Service is not configured. Base URL missing.");
		}
		const baseUrl =
			this.baseUrl || process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL;
		const url = `${baseUrl}${endpoint}`;

		const headers: HeadersInit = {
			"Content-Type": "application/json",
			Accept: "application/json",
		};

		const opt: RequestInit = {
			method,
			headers,
			credentials: requiresAuth ? "include" : "omit",
		};

		if (body) {
			opt.body = JSON.stringify(body);
		}

		try {
			const response = await fetch(url, opt);
			console.log(`headers`, response);
			const accessToken = response.headers.get("testaccess");
			if (accessToken) {
				Cookie.set("testaccessa", accessToken, {
					expires: 1,
					path: "/",
				});
			}

			let data: any;
			try {
				data = await response.json();
			} catch (jsonError) {
				data = {
					status: response.status,
					message: `Invalid JSON received from server for ${url}.`,
				};
			}

			return {
				status: response.status,
				ok: response.ok,
				data,
			} as unknown as T;
		} catch (networkError: any) {
			console.error(`Network error during fetch to ${url}:`, networkError);
			return {
				status: 500,
				ok: false,
				data: {
					message: networkError.message || "Network error occurred.",
				},
			} as unknown as T;
		}
	}

	public async login(
		email: string,
		password: string
	): Promise<{ message: string; status: number; token?: string }> {
		return this.request<{ message: string; status: number; token?: string }>(
			`/user/login`,
			`POST`,
			{ email, password },
			true
		);
	}
}
