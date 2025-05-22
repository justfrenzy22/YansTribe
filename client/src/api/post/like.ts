import { IResponse } from "@/types/IResponse";
import { ApiService } from "../apiService";

const apiService = new ApiService();

const likePost = async (postId: string, cookie: string | null): Promise<IResponse> =>
	apiService.request<IResponse>({
		endpoint: `/post/like/${postId}`,
		method: `GET`,
		options: { requiresAuth: true, cookie: cookie },
	});

export default likePost;
