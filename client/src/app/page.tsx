import Footer from "@/components/auth/footer";
import { Navbar } from "@/components/custom/navbar";
import PostFeed from "@/components/home/post-feed";
import WaitLayout from "@/components/home/waiting-animation";
import DeviceProvider from "@/contexts/DeviceContext";
import { headers } from "next/headers";

const useData = async () => {
	try {
		const res = await fetch("https://jsonplaceholder.typicode.com/posts");
		return await res.json();
	} catch (error) {
		console.error("Error fetching data:", error);
		return null;
	}
};

const Home = async () => {
	const data = await useData();
	const userAgent = (await headers()).get("user-agent") || "";

	return (
		<DeviceProvider userAgent={userAgent}>
			<WaitLayout data={data}>
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
						{/* md:space-y-4  */}
							{/* <StoriesBar /> */}
							
							<PostFeed />
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
			</WaitLayout>
		</DeviceProvider>
	);
};

export default Home;
