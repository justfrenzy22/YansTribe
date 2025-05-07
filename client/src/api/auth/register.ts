import { ApiService } from "../apiService";

const apiService = new ApiService();

const register = async (
	username: string,
	email: string,
	password: string,
	full_name: string,
	bio: string,
	location: string,
	website: string
): Promise<{
	message: string;
	status: number;
}> =>
	apiService.request<{ message: string; status: number }>({
		endpoint: `/user/register`,
		method: `POST`,
		options: {
			body: {
				username,
				email,
				password,
				full_name,
				bio,
				location,
				website,
			},
			requiresAuth: false,
		},
	});

export default register;

// `POST`,
// { body: { username, email, password, full_name, bio, location, website } }
