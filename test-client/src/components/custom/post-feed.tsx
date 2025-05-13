import { IPost } from "@/types/post/IPost";
import Post from "../custom/post";
// import { Separator } from "../ui/separator";
// import CreatePost from "./create-post";

const PostFeed = () => {
	const posts = [
		{
			post_id: "asdadssad",
			user_id: "as3aed",
			username: "JohnDoe",
			pfp_src: "/images/user1.jpg",
			content:
				"Just shared some new photos from my recent trip! What do you think? #travel #photography",
			created_at: new Date(),
			media: [
				{
					media_id: "asd",
					media_type: 0,
					media_src: "./code.jpg",
					post_id: "asdadssad",
				},
				{
					media_id: `asdasd`,
					media_type: 0,
					media_src: "/images/trip2.jpg",
					post_id: "asdadssad",
				},
			],
		} as IPost,
		{
			post_id: `asdasd`,
			user_id: `asdhuaseasnd`,
			username: "JaneSmith",
			pfp_src: "/images/user2.jpg",
			content:
				"Working on an exciting new project. Can't wait to share more details soon! #coding #webdev",
			created_at: new Date(),
			media: [
				{
					media_id: `asdasd`,
					media_type: 1,
					media_src: "/videos/project-preview.mp4",
				},
			],
		} as IPost,
	];

	return (
		<div className="md:space-y-4 space-y-0 justify-center pb-20 md:pb-0 rounded-t-3xl bg-secondary/45 dark:bg-secondary/45 border outline shadow-lg">
			{/* <CreatePost /> */}
			{posts.map((post, idx) => (
				<div key={idx}>
					<Post post={post} idx={idx}  />
					{/* <Separator orientation="horizontal" className="w-full border-1 my-4" /> */}
				</div>
			))}
		</div>
	);
};

export default PostFeed;
