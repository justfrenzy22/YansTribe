"use server";
import { ApiService } from "../apiService";

const apiService = new ApiService();

const cancelFriend = async (
	user2_id: string,
	cookie: string | null
): Promise<{
	message: string;
	status: number;
}> =>
	apiService.request<{ message: string; status: number }>({
		endpoint: `/friend/cancel/${user2_id}`,
		method: `GET`,
		options: { requiresAuth: true, cookie: cookie },
	});

export default cancelFriend;
