// import Footer from "@/components/auth/footer";
import getUserById from "@/api/auth/getUserById";
import getHomePosts from "@/api/post/get-home";
import { Navbar } from "@/components/custom/navbar";
import PostFeed from "@/components/home/post-feed";
import WaitLayout from "@/components/home/waiting-animation";
import { UserProvider } from "@/contexts/UserContext";
import UseTokenHook from "@/hooks/contexts/useTokenHook";
import { IUserResponse } from "@/types/IResponse";

const Home = async () => {
	const { data, posts } = await (async () => {
		const data: IUserResponse = await getUserById(await UseTokenHook());
		const posts = await getHomePosts(await UseTokenHook());
		return { data: data, posts: posts.posts };
	})();

	return (
		<WaitLayout data={data}>
			<UserProvider user={data.user}>
				<Navbar />
				<main className="container mx-auto pt-16 md:px-4 px-2">
					<div className="flex gap-4">
						{/* Left Sidebar - hidden on mobile */}
						<div className="hidden md:block w-1/4 lg:w-1/5">
							<div className="sticky top-20">
								{/* <Sidebar /> */}
								sidebar
							</div>
						</div>

						{/* Main Content */}
						<div className="w-full md:w-2/4 lg:w-3/5 space-y-0">
							<PostFeed posts={posts ?? []} />
						</div>

						{/* Right Sidebar - hidden on mobile */}
						<div className="hidden md:block w-1/4 lg:w-1/5">
							<div className="sticky top-20">
								{/* <RightSidebar /> */}
								right bar
							</div>
						</div>
					</div>
				</main>
			</UserProvider>
		</WaitLayout>
	);
};

export default Home;
