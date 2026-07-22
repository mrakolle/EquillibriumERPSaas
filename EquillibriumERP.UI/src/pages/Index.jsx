
import Navbar from "../components/home/Navbar";
import HeroSection from "../components/home/HeroSection";
import FeatureGrid from "../components/home/FeatureGrid";
import PricingSection from "../components/home/PricingSection";
import WhySection from "../components/home/WhySection";
import CallToAction from "../components/home/CallToAction";
import Footer from "../components/home/Footer";
//import Index from "./pages/index";


import "../components/home/home.css";

function Index() {
    return (
        <main className="home-page">
            <Navbar />

            <HeroSection />

            <FeatureGrid />

            <PricingSection />

            <WhySection />

            <CallToAction />

            <Footer />
        </main>
    );
}

export default Index;