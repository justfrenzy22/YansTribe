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

	private async _req<T>(
		endpoint: string,
		method: string = "GET",
		body: any = null,
		requiresAuth: boolean = false // Flag indicating if the request needs the auth cookie
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
			if (response.ok) {
				if (response.status === 204) {
					return null as T;
				}
				// console.log(`ApiService headers`, response.headers.all());
				// if (
				// 	response.headers.get("testaccess") === undefined ||
				// 	response.headers.get("testaccess") === null
				// ) {
				// 	alert(`asdasdads`);
				// 	throw new Error("Access token not found in response headers.");
				// } else {
				// 	alert(`asdasjdnjasndjkasndkjanskdjajkdnsajkja`);
				// }
				// } else {
				const accessToken = response.headers.get("testaccess");
				Cookie.set("testaccessa", accessToken as string, {
					expires: 1,
					path: "/",
				});
				try {
					const data = await response.json();

					return data as T;
				} catch (jsonError) {
					console.error(
						`Failed to parse JSON response from ${url}:`,
						jsonError
					);
					return JSON.stringify({
						status: 500,
						message: `Invalid JSON received from server for ${url}.`,
					}) as T;
					// throw new Error(`Invalid JSON received from server for ${url}.`);
				}
			} else {
				let errorMessage = `Request failed with status ${response.status}`;
				let errorData: any = null;

				if (response.status === 401 && requiresAuth) {
					errorMessage =
						"Unauthorized. Session may have expired or cookie is missing/invalid.";
					try {
						errorData = await response.json();
					} catch (e) {}
				} else {
					try {
						errorData = await response.json();
						errorMessage =
							errorData.message || errorData.title || JSON.stringify(errorData);
					} catch (e) {
						try {
							const textError = await response.text();
							if (textError) errorMessage += `: ${textError}`;
						} catch (textE) {}
					}
				}

				// const err = new Error(errorMessage);
				// (err as any).status = response.status;
				// (err as any).data = errorData;
				// throw err;
				return response as unknown as T;
			}
		} catch (networkError: any) {
			console.error(`Network error during fetch to ${url}:`, networkError);
			if (
				networkError instanceof TypeError &&
				networkError.message.includes("fetch")
			) {
				if (opt.credentials === "include") {
					throw new Error(
						`Network Error: Failed to fetch. Check CORS configuration on the backend (specifically Access-Control-Allow-Credentials) or server status for ${url}.`
					);
				} else {
					throw new Error(
						`Network Error: Failed to fetch. Check server status or network connection for ${url}.`
					);
				}
			}
			throw networkError;
		}
	}

	public async login(
		email: string,
		password: string
	): Promise<{ message: string; status: number; token?: string }> {
		return this._req<{ message: string; status: number; token?: string }>(
			`/user/login`,
			`POST`,
			{ email, password },
			true
		);
	}

	public async register(
		username: string,
		email: string,
		password: string,
		full_name: string,
		bio: string,
		location: string,
		website: string,
	) : Promise<{ message: string; status: number }> {
		return this._req<{ message: string; status: number }>(
			`/user/register`,
			`POST`,
			{ username, email, password, full_name, bio, location, website },
			false
		);
	}


	// public async register (
	// 	email: string,
	// 	password: string,
	// 	fullName: string,
	// 	bio: string,
	// )
}
