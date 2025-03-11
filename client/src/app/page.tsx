import { Navbar } from "@/components/custom/navbar";
import WaitLayout from "@/components/home/waiting-animation";
import DeviceProvider from "@/contexts/DeviceContext";
import { headers } from "next/headers";



async function useData() {
	try {
		const res = await fetch("https://jsonplaceholder.typicode.com/posts" ); // Disable caching
		// { cache: "no-store" }
		return await res.json();
	} catch (error) {
		console.error("Error fetching data:", error);
		return null;
	}
}


export default async function Home() {
	const data = await useData(); // Fetch data on the server side
	const userAgent = (await headers()).get("user-agent") || ""; // Get user agent

	return (
		<DeviceProvider userAgent={userAgent}>
			<WaitLayout data={data}>
				<Navbar />
			</WaitLayout>
		</DeviceProvider>
	);
}