import { IPost } from "@/types/post/IPost";
import useAsyncHandler from "./useAsyncHandler";
import { IGetCommentsResponse, IResponse } from "@/types/IResponse";
import UseTokenHook from "../contexts/useTokenHook";
import likePost from "@/api/post/like";
import dislikePost from "@/api/post/dislike";
import addCommentAsync from "@/api/comment/addComment";
import { IBaseUser } from "@/types/IBaseUser";
import { IComment, IBaseComment } from "@/types/comment/IComment";
import { useCallback } from "react";
import getCommentsAsync from "@/api/comment/getComments";

const usePostActions = ({
	post,
	setPost,
}: {
	post: IPost;
	setPost: React.Dispatch<React.SetStateAction<IPost>>;
}) => {
	const { isLoading, handleAsync } = useAsyncHandler();

	const getToken = UseTokenHook;

	// Generic state update handler for like/dislike
	const handleReaction = useCallback(
		async (
			apiFunc: (postId: string, token: string) => Promise<IResponse>,
			updatePost: (prev: IPost) => IPost
		) => {
			const token = await getToken();
			await handleAsync(
				() => apiFunc(post.post_id, token as string),
				(res): Promise<IResponse> => {
					setPost((prev) => updatePost(prev!));
					return Promise.resolve(res as IResponse);
				}
			);
		},
		[post.post_id, setPost, getToken, handleAsync]
	);

	const like = () =>
		handleReaction(likePost, (prev) => ({
			...prev,
			liked: true,
			likes: prev.like_count + 1,
		}));

	const dislike = () =>
		handleReaction(dislikePost, (prev) => ({
			...prev,
			liked: false,
			likes: Math.max(0, prev.like_count - 1),
		}));

	const addComment = useCallback(
		async (
			user_id: IBaseUser["user_id"],
			content: string,
			updateComments: (newComment: IComment) => void
		) => {
			const token = await getToken();
			await handleAsync(
				() => addCommentAsync(post.post_id, user_id, content, token as string),
				() => {
					const newComment: IBaseComment = {
						// comment_id: "",
						post_id: post.post_id,
						user: post.user,
						content,
						created_at: new Date().toString(),
					};
					updateComments(newComment as IComment);
				}
			);
		},
		[post.post_id, post.user, getToken, handleAsync]
	);

	const getComments = useCallback(
		async (
			post_id: IPost[`post_id`],
			updateComments: (comments: IComment[]) => void
		) => {
			const token = await getToken();
			await handleAsync<IGetCommentsResponse>(
				() => getCommentsAsync(post_id, token as string),
				(res) => updateComments(res.comments)
			);
		},
		[getToken, handleAsync]
	);

	// Grouped image handlers for DRYness
	const imageHandlers = {
		onLoad: (
			media_id: string,
			setLoading: React.Dispatch<React.SetStateAction<Record<string, boolean>>>
		) => setLoading((prev) => ({ ...prev, [media_id]: false })),

		onError: (
			media_id: string,
			setLoading: React.Dispatch<React.SetStateAction<Record<string, boolean>>>
		) => {
			console.error(`Failed to load image: ${media_id}`);
			setLoading((prev) => ({ ...prev, [media_id]: false }));
		},
	};

	const handleShare = (postId: string) => {
		console.log(`Post ${postId} shared`);
	};

	return {
		isLoading,
		like,
		dislike,
		addComment,
		getComments,
		handleShare,
		imageHandlers,
	};
};

export default usePostActions;
