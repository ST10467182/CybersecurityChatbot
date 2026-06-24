// =============================================================
// ResponseEngine.cs
// Cybersecurity Awareness Chatbot - Response Engine
// =============================================================
// Handles keyword-based response matching with random selection.
// Carried over from Part 2 with full 30-topic coverage.
// Uses Dictionary<string, (keywords, List<string>, followUp)>.
//
// References:
// [3] Pieterse, H. 2021. The Cyber Threat Landscape in South
//     Africa: A 10-Year Review. The African Journal of
//     Information and Communication, 28(28).
//     doi: https://doi.org/10.23962/10539/32213
//     [Accessed 16 February 2026].
// [4] SABRIC, 2023. Cybercrime [Online].
//     Available at: https://www.sabric.co.za [Accessed 14 April 2026].
// [5] NCPF, 2015. Department of Telecommunications [Online].
//     Available at: https://www.gov.za [Accessed 14 April 2026].
// [9] Information Regulator, 2021. POPIA [Online].
//     Available at: https://www.inforegulator.org.za [Accessed 14 April 2026].
// [10] SAPS, 2024. Available at: https://www.saps.gov.za [Accessed 14 April 2026].
// =============================================================

using System;
using System.Collections.Generic;

namespace CybersecurityChatbotGUI
{
    public class ResponseEngine
    {
        private readonly Dictionary<string, (string[] Keywords, List<string> Responses, string FollowUp)> _topics;
        private readonly Random _random;
        private string _lastMatchedTopic = "";

        public ResponseEngine()
        {
            _random = new Random();
            _topics = new Dictionary<string, (string[], List<string>, string)>
            {
                ["phishing"] = (
                    new[] { "phishing", "phish", "fake email", "suspicious email", "spam email" },
                    new List<string>
                    {
                        "Phishing is one of the most common cybercrimes in South Africa [3]. Attackers send fake emails pretending to be banks or SARS to steal your info. Always verify the sender's actual email address.",
                        "Watch out for urgency in emails — 'Your account will be closed!' Legitimate organisations never pressure you like this [3]. Go directly to the official website rather than clicking any link.",
                        "Phishing costs South African businesses billions annually [3]. Email spoofing makes the email look exactly like your bank's — but the sender address is slightly different. Always check carefully."
                    },
                    "Hover over any link before clicking. The real URL appears at the bottom of your browser. If it doesn't match the sender's official domain, do not click it."
                ),
                ["password"] = (
                    new[] { "password", "passphrase", "credentials", "strong password" },
                    new List<string>
                    {
                        "Strong passwords are your first line of defence. Use at least 12 characters mixing uppercase, lowercase, numbers, and symbols. Use a unique password for every account [4].",
                        "Think of a password like a toothbrush — personal, changed regularly, and never shared. A passphrase (four random words) is more secure and easier to remember than a short complex password.",
                        "Weak passwords are responsible for a large percentage of data breaches [4]. Avoid '123456' or your pet's name. Enable 2FA as a backup layer."
                    },
                    "Password managers like Bitwarden (free) generate strong, unique passwords for every site. You only need to remember one master password."
                ),
                ["malware"] = (
                    new[] { "malware", "ransomware", "virus", "spyware", "trojan" },
                    new List<string>
                    {
                        "Malware is malicious software designed to damage or gain unauthorised access to your system [3]. Keep your antivirus updated and back up your data regularly.",
                        "Ransomware has heavily targeted South African businesses — it encrypts your files and demands payment [3]. Regular offline backups are your best protection. Never pay the ransom.",
                        "Trojans disguise themselves as legitimate software. Only download from official sources like the Microsoft Store, Google Play, or the developer's official website."
                    },
                    "Combine antivirus with regular updates, safe browsing habits, and data backups — this is called 'defence in depth'."
                ),
                ["safe browsing"] = (
                    new[] { "browsing", "safe browser", "internet safety", "https", "url", "website" },
                    new List<string>
                    {
                        "Safe browsing starts with 'https://' — the 's' means the connection is encrypted. Look for the padlock icon in your browser bar [5].",
                        "Keep your browser updated — updates patch vulnerabilities that criminals actively exploit. Use Chrome, Firefox, or Edge with built-in security features.",
                        "Avoid clicking on pop-up ads — they can lead to phishing sites. If a website seems suspicious, trust your instincts and leave immediately."
                    },
                    "Private/incognito mode doesn't make you anonymous. For real privacy, combine a VPN with a privacy-focused browser."
                ),
                ["social engineering"] = (
                    new[] { "social engineering", "manipulation", "pretexting", "impersonat", "scam" },
                    new List<string>
                    {
                        "Social engineering exploits human trust rather than technical vulnerabilities [3]. Attackers may pose as IT support or a government official. Always verify identities before sharing information.",
                        "Pretexting is a common technique — the attacker creates a fabricated scenario to extract information, like calling pretending to be from your bank's fraud department."
                    },
                    "The best defence is healthy scepticism. It's not rude to verify someone's identity — it's smart. Any legitimate professional will understand."
                ),
                ["2fa"] = (
                    new[] { "two factor", "2fa", "multi factor", "mfa", "otp", "one time pin" },
                    new List<string> { "Two-factor authentication (2FA) adds a second security layer beyond your password [4]. Even if someone gets your password, they cannot access your account without the OTP. Enable it on email, banking, and social media." },
                    "Use an authenticator app like Google Authenticator rather than SMS-based OTPs — SIM swapping makes SMS 2FA vulnerable."
                ),
                ["public wifi"] = (
                    new[] { "wifi", "wi-fi", "public wifi", "hotspot" },
                    new List<string> { "Public Wi-Fi is a hunting ground for cybercriminals [3]. Never access online banking on public Wi-Fi. Always use a VPN. Turn off auto-connect and forget the network after use." },
                    "Mobile data is generally safer than public Wi-Fi for banking and personal accounts."
                ),
                ["privacy"] = (
                    new[] { "privacy", "popia", "personal information", "data protection" },
                    new List<string> { "Data privacy is your legal right in South Africa, protected by POPIA [9]. Read privacy policies, limit what you share online, and report data misuse to the Information Regulator." },
                    "Under POPIA, organisations must tell you why they collect your data, keep it secure, and delete it when no longer needed."
                ),
                ["identity theft"] = (
                    new[] { "identity theft", "identity fraud", "stolen identity", "id theft" },
                    new List<string> { "Identity theft occurs when a criminal uses your personal details to impersonate you [4]. Never share your ID number unnecessarily. Check your credit report regularly. Report to SAPS immediately [10]." },
                    "Place a fraud alert with credit bureaus like TransUnion or Experian if your identity is compromised."
                ),
                ["cyberbullying"] = (
                    new[] { "cyberbullying", "cyber bully", "online harassment", "trolling" },
                    new List<string> { "Cyberbullying involves using digital platforms to harass or humiliate someone [5]. Do not respond. Screenshot evidence. Report to the platform and SAPS [10]." },
                    "South Africa's Cybercrimes Act makes online harassment a criminal offence. Document everything — you have legal protection."
                ),
                ["online shopping"] = (
                    new[] { "online shopping", "shopping online", "e-commerce", "buy online" },
                    new List<string> { "Online shopping fraud is among the fastest growing cybercrimes in SA [4]. Only use established retailers. Check for 'https://' and the padlock. Use a credit card rather than a direct bank transfer." },
                    "If a deal seems too good to be true online, it almost certainly is."
                ),
                ["email safety"] = (
                    new[] { "email safety", "email security", "safe email", "email tips" },
                    new List<string> { "Email is the most exploited communication channel in South Africa [3]. Never open attachments from unknown senders. Hover over links before clicking. Enable spam filtering." },
                    "Use an email alias for sign-ups to keep your real email private."
                ),
                ["software updates"] = (
                    new[] { "update", "software update", "patch", "outdated software" },
                    new List<string> { "Unpatched software is one of the most common attack vectors [5]. Enable automatic updates for your OS and apps. Never ignore update notifications." },
                    "The WannaCry ransomware attack infected hundreds of thousands of computers because users had not applied an available security update."
                ),
                ["backup"] = (
                    new[] { "backup", "back up", "data recovery", "restore data" },
                    new List<string> { "Follow the 3-2-1 backup rule: 3 copies, on 2 different media, with 1 stored offsite [5]. Test your backups regularly. Automate them so you never forget." },
                    "Ransomware is only devastating if you have no backups. A clean backup means you can restore without paying a ransom."
                ),
                ["firewall"] = (
                    new[] { "firewall", "fire wall", "network protection" },
                    new List<string> { "A firewall monitors and controls network traffic based on security rules [5]. Keep your OS firewall on at all times. Use a hardware firewall on your home router." },
                    "Most modern routers include a built-in hardware firewall. Make sure it is enabled in router settings."
                ),
                ["encryption"] = (
                    new[] { "encryption", "encrypt", "encrypted", "end-to-end" },
                    new List<string> { "Encryption converts your data into unreadable code that only authorised parties can decode [5]. Use end-to-end encrypted messaging apps. Enable full-disk encryption on your devices." },
                    "WhatsApp and Signal both use end-to-end encryption — not even the companies can read your messages."
                ),
                ["dark web"] = (
                    new[] { "dark web", "darkweb", "deep web", "tor browser" },
                    new List<string> { "The dark web is hidden from standard search engines and used for both privacy and criminal activity [3]. Check haveibeenpwned.com to see if your data has been exposed." },
                    "Law enforcement actively monitors the dark web. Many criminals thought to be anonymous have been identified and arrested."
                ),
                ["shoulder surfing"] = (
                    new[] { "shoulder surf", "shoulder-surf", "someone watching", "spying on screen" },
                    new List<string> { "Shoulder surfing is when someone watches your screen to steal PINs [4]. Shield your screen at ATMs. Use a privacy screen protector. Log out of sensitive accounts immediately after use." },
                    "Always cover the ATM keypad with your other hand when entering your PIN."
                ),
                ["usb"] = (
                    new[] { "usb", "flash drive", "removable media", "thumb drive" },
                    new List<string> { "USB drives can carry malware without an internet connection [3]. Never plug in a USB you found unexpectedly. Scan all external drives with antivirus first." },
                    "The Stuxnet worm spread via infected USB drives — even air-gapped systems can be compromised through removable media."
                ),
                ["children online"] = (
                    new[] { "children", "kids online", "child safety", "parental control" },
                    new List<string> { "Children are particularly vulnerable to grooming and cyberbullying [5]. Enable parental controls. Keep devices in shared spaces. Teach children never to share personal info with strangers." },
                    "Have regular, open conversations with children about their online experiences."
                ),
                ["online banking"] = (
                    new[] { "online banking", "internet banking", "bank app", "banking safety" },
                    new List<string> { "Online banking fraud costs South Africa billions annually [4]. Only access banking on your personal device. Type your bank's URL directly. Log out completely after every session." },
                    "Set daily transaction limits on your online banking profile to limit the damage if someone gains access."
                ),
                ["deepfakes"] = (
                    new[] { "deepfake", "deep fake", "ai scam", "fake video", "voice cloning" },
                    new List<string> { "Deepfakes use AI to create convincing fake videos or voice recordings [3]. Verify urgent requests through a second channel. Be sceptical of shocking viral content." },
                    "CEO fraud via voice cloning is a growing threat. Always verify large financial requests through an independent channel."
                ),
                ["sim swap"] = (
                    new[] { "sim swap", "sim swapping", "sim hijack", "sim card fraud" },
                    new List<string> { "SIM swapping is when criminals convince your network to transfer your number to their SIM, giving them your OTPs [4]. Set a SIM swap PIN with your provider." },
                    "Use an authenticator app instead of SMS-based 2FA to protect against SIM swapping."
                ),
                ["account hacking"] = (
                    new[] { "hacked", "account hack", "my account was", "compromised account" },
                    new List<string> { "If hacked: change your password immediately, enable 2FA, check unrecognised logins, warn your contacts, and report to SAPS [10]." },
                    "After securing your account, revoke permissions from all unrecognised third-party apps."
                ),
                ["cookies"] = (
                    new[] { "cookie", "tracking", "online tracking", "browser history" },
                    new List<string> { "Cookies track your browsing behaviour [9]. Decline non-essential cookies. Clear browser cache regularly. Use tracker-blocking extensions." },
                    "Browser fingerprinting identifies you without cookies — a privacy browser like Brave blocks this by default."
                ),
                ["cloud security"] = (
                    new[] { "cloud", "cloud storage", "google drive", "onedrive", "dropbox" },
                    new List<string> { "Cloud storage carries risks if misconfigured [5]. Use strong unique passwords and enable 2FA. Don't store unencrypted sensitive documents in the cloud." },
                    "Regularly audit your cloud storage sharing settings — many breaches are caused by accidentally public folders."
                ),
                ["reporting"] = (
                    new[] { "report", "cybercrime report", "report crime", "where to report" },
                    new List<string> { "Report cybercrime to SAPS at your nearest station or call 10111 [10]. Report banking fraud to your bank's fraud hotline. Report data breaches to the Information Regulator [9]." },
                    "The South African Fraud Prevention Service (SAFPS) at safps.org.za also accepts fraud and identity theft reports."
                ),
                ["vpn"] = (
                    new[] { "vpn", "virtual private network" },
                    new List<string> { "A VPN encrypts your internet traffic and hides your IP address [5]. Use a reputable paid VPN — free VPNs often sell your data. Always use a VPN on public Wi-Fi." },
                    "Reputable VPN providers include NordVPN, ProtonVPN, and Surfshark. Avoid free VPNs."
                ),
                ["smishing"] = (
                    new[] { "smishing", "sms scam", "text message scam", "fake sms" },
                    new List<string> { "Smishing is phishing via SMS [3]. Never click links in unexpected SMS messages. SARS and banks will never send account links via SMS [4]." },
                    "A common SA smishing scam involves fake SARS refund SMSes. SARS communicates via eFiling — never via SMS links."
                ),
                ["social media"] = (
                    new[] { "social media", "facebook", "instagram", "twitter", "tiktok", "linkedin" },
                    new List<string> { "Social media exposes personal data that criminals exploit [3]. Set profiles to private. Never share your location or ID number publicly." },
                    "Oversharing on social media — your pet's name, school — gives attackers answers to your security questions."
                ),
                ["topics"] = (
                    new[] { "topics", "what can i ask", "menu", "help me", "what can you help" },
                    new List<string>
                    {
                        "Here are all 30 topics you can ask me about:\n\n" +
                        "  1. Phishing        2. Passwords       3. Malware\n" +
                        "  4. Safe Browsing   5. Social Eng.     6. 2FA\n" +
                        "  7. Public Wi-Fi    8. Data Privacy    9. Identity Theft\n" +
                        " 10. Cyberbullying  11. Online Shopping 12. Email Safety\n" +
                        " 13. SW Updates     14. Backups        15. Firewalls\n" +
                        " 16. Encryption     17. Dark Web       18. Shoulder Surfing\n" +
                        " 19. USB Safety     20. Children Online 21. Online Banking\n" +
                        " 22. Deepfakes      23. SIM Swapping   24. Account Hacking\n" +
                        " 25. Cookies        26. Cloud Security 27. Report Cybercrime\n" +
                        " 28. VPNs           29. Smishing       30. Social Media\n\n" +
                        "  Or try: 'add task', 'start quiz', 'show activity log'"
                    },
                    ""
                ),
                ["general"] = (
                    new[] { "how are you", "how r u", "how do you feel", "are you okay" },
                    new List<string> { "I'm fully operational and ready to help! South Africa's cyber threat landscape continues to evolve [3]. What topic can I assist you with?" },
                    ""
                ),
                ["purpose"] = (
                    new[] { "your purpose", "what do you do", "what are you", "who are you" },
                    new List<string> { "I'm a Cybersecurity Awareness Bot designed to educate South African citizens on identifying and avoiding online threats [3]. Type 'topics' to see the full list." },
                    ""
                ),
            };
        }

        public string? GetResponse(string userInput)
        {
            string input = userInput.ToLower().Trim();
            foreach (var topic in _topics)
            {
                foreach (string keyword in topic.Value.Keywords)
                {
                    if (input.Contains(keyword))
                    {
                        _lastMatchedTopic = topic.Key;
                        var responses = topic.Value.Responses;
                        return responses[_random.Next(responses.Count)];
                    }
                }
            }
            _lastMatchedTopic = "";
            return null;
        }

        public string GetLastMatchedTopic() => _lastMatchedTopic;

        public string? GetFollowUp(string topic)
        {
            if (_topics.ContainsKey(topic) && !string.IsNullOrEmpty(_topics[topic].FollowUp))
                return _topics[topic].FollowUp;
            return null;
        }
    }
}
