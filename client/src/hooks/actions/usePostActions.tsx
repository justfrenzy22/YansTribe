import { IPost } from "@/types/post/IPost";
import useAsyncHandler from "./useAsyncHandler";
import { IResponse } from "@/types/IResponse";
import UseTokenHook from "../contexts/useTokenHook";
import likePost from "@/api/post/like";
import dislikePost from "@/api/post/dislike";

const usePostActions = ({
	post,
	setPost,
}: {
	post: IPost;
	setPost: React.Dispatch<React.SetStateAction<IPost>>;
}) => {
	const { isLoading, handleAsync } = useAsyncHandler();

	const handleReaction = async (
		apiFunc: (postId: string, token: string) => Promise<IResponse>,
		updateFunc: (prev: IPost) => IPost
	) => {
		const token = await UseTokenHook();
		await handleAsync(
			() => apiFunc(post.post_id, token as string),
			() => setPost((prev) => updateFunc(prev))
		);
	};

	const handleShare = (postId: string) => {
		// Logic to share a post
		console.log(`Post ${postId} shared`);
	};

	const ImageLoad = (
		media_id: string,
		setLoadingImages: React.Dispatch<
			React.SetStateAction<Record<string, boolean>>
		>
	) => {
		setLoadingImages((prev) => ({ ...prev, [media_id]: false }));
	};

	const ImageError = (
		media_id: string,
		setLoadingImages: React.Dispatch<
			React.SetStateAction<Record<string, boolean>>
		>
	) => {
		console.error(`Failed to load image with media_id: ${media_id}`);
		setLoadingImages((prev) => ({ ...prev, [media_id]: false }));
	};

	return {
		handleShare,
		ImageLoad,
		ImageError,
		isLoading,
		like: () =>
			handleReaction(likePost, (post) => ({
				...post,
				liked: true,
				likes: post.like_count + 1,
			})),
		dislike: () =>
			handleReaction(dislikePost, (post) => ({
				...post,
				liked: false,
				likes: post.like_count > 0 ? post.like_count - 1 : 0,
			})),
	};
};

export default usePostActions;
