import Post from "../custom/post";
import { Separator } from "../ui/separator";

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
		},
	];

	return (
		<div className="md:space-y-4 space-y-0 justify-center pb-20 md:pb-0 rounded-t-3xl bg-secondary/45 dark:bg-secondary/45 border outline shadow-lg">
			{/* <Post */}
			<div className="p-4">
				some stories bar or something like taht some stories bar or something
				like taht some stories bar or something like taht some stories bar or
				something like taht
			</div>
			{posts.map((post, idx) => {
				return (
					<Post post={post} key={idx}>
						{/* <Separator
							orientation="horizontal"
							className="w-full border-1 mb-4 "
						/> */}
						{/* <div className=" space-y-4">
							<Post post={post} />
						</div> */}
					</Post>
				);
			})}
		</div>
	);
};

export default PostFeed;
