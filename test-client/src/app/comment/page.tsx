import Comment from "@/components/custom/comment";
import { Card } from "@/components/ui/card";
import DeviceProvider from "@/contexts/DeviceContext";
import { IComment } from "@/types/IComment";
import { headers } from "next/headers";

const CommentPage = async () => {
	const userAgent =
		((await headers()).get(`user-agent`) as string | undefined) ?? ``;

	const currentUserId = "user_1";

	const dummyComment: IComment = {
		comment_id: "cmt_123",
		user_id: "user_1",
		username: "yordan_todorov",
		pfp_src: "https://api.dicebear.com/7.x/thumbs/svg?seed=Yordan",
		created_at: "2h ago",
		content: "This is a dummy comment for testing the UI components.",
		liked: false,
		liked_count: 12,
		reply_count: 3,
	};

	const dummyComment2: IComment = {
		comment_id: "cmt_456",
		user_id: "user_2",
		username: "maria_ilieva",
		pfp_src: "https://api.dicebear.com/7.x/thumbs/svg?seed=Maria",
		created_at: "15m ago",
		content:
			"Honestly, I think this is one of the best discussions I’ve seen here!",
		liked: true,
		liked_count: 84,
		reply_count: 5,
	};

	return (
		<DeviceProvider userAgent={userAgent}>
			<div className="h-screen w-full flex flex-col justify-center items-center">
				<Card className="w-[600px] m-auto p-4">
					<Comment comment={dummyComment} currentUserId={currentUserId} />
					<Comment comment={dummyComment2} currentUserId={currentUserId} />
				</Card>
			</div>
		</DeviceProvider>
	);
};

export default CommentPage;
