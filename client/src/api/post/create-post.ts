import { ApiService } from "../apiService";

const apiService = new ApiService();

const createPost = async (
	cookie: string | null,
	form: FormData
): Promise<{
	message: string;
	status: number;
}> => {
	return await apiService.request<{ message: string; status: number }>({
		endpoint: `/post/create_post`,
		method: `POST`,
		options: {
			form: form,
			files: form.getAll("files").length > 0,
			requiresAuth: true,
			cookie: cookie,
		},
	});
};

export default createPost;
