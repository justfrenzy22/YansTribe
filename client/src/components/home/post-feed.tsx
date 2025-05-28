"use client";
import { IPost } from "@/types/post/IPost";
import Post from "../custom/post";
import CreatePost from "./create-post";
import { useEffect, useState, lazy, Suspense } from "react";

const LazyPostModal = lazy(() => import("../custom/post-modal"));

const PostFeed = ({ posts }: { posts: IPost[] }) => {
	const [selectedPost, setSelectedPost] = useState<IPost>({} as IPost);
	const [isPostModalVisible, setIsPostModalVisible] = useState(false);

	useEffect(() => {
		if (selectedPost && Object.keys(selectedPost).length > 0) {
			setIsPostModalVisible(true);
		}
	}, [selectedPost]);

	return (
		<div className="md:space-y-4 space-y-0 justify-center pb-20 md:pb-0 rounded-t-3xl bg-secondary/45 dark:bg-secondary/45 border outline shadow-lg">
			<CreatePost />
			{posts.length === 0 && <p>No posts yet</p>}

			{posts.length && (
				<div>
					{posts.map((post, idx) => (
						<div key={idx}>
							<Post initPost={post} setSelectedPost={setSelectedPost} />
						</div>
					))}
					{isPostModalVisible == true && (
						<Suspense fallback={null}>
							<LazyPostModal
								post={selectedPost}
								setPost={setSelectedPost}
								visible={isPostModalVisible}
								setVisible={setIsPostModalVisible}
							/>
						</Suspense>
					)}
				</div>
			)}
		</div>
	);
};

export default PostFeed;
