import Post from "../custom/post";
import { Separator } from "../ui/separator";
import CreatePost from "./create-post";

const PostFeed = () => {
	const posts = [
		{
			post_id: 1,
			user: {
				user_id: 1,
				username: "JohnDoe",
				pfp_src: "/images/user1.jpg",
			},
			content:
				"Just shared some new photos from my recent trip! What do you think? #travel #photography",
			created_at: "2023-10-01T12:00:00Z",
			media: [
				{ type: "image", url: "./code.jpg" },
				{ type: "image", url: "/images/trip2.jpg" },
			],
		},
		{
			post_id: 2,
			user: {
				user_id: 2,
				username: "JaneSmith",
				pfp_src: "/images/user2.jpg",
			},
			content:
				"Working on an exciting new project. Can't wait to share more details soon! #coding #webdev",
			created_at: "2023-10-02T14:30:00Z",
			media: [{ type: "video", url: "/videos/project-preview.mp4" }],
		},
	];

	return (
		<div className="md:space-y-4 space-y-0 justify-center pb-20 md:pb-0 rounded-t-3xl bg-secondary/45 dark:bg-secondary/45 border outline shadow-lg">
			<CreatePost />
			{posts.map((post, idx) => (
				<div key={idx}>
					<Post post={post} />
					{/* <Separator orientation="horizontal" className="w-full border-1 my-4" /> */}
				</div>
			))}
		</div>
	);
};

export default PostFeed;
