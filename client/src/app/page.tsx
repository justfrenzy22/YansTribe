import Footer from "@/components/auth/footer";
import { Navbar } from "@/components/custom/navbar";
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
				<Footer />
			</WaitLayout>
		</DeviceProvider>
	);
};

export default Home;
