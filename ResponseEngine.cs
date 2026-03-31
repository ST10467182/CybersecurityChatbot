// =============================================================
// ResponseEngine.cs
// Cybersecurity Awareness Chatbot - Response Logic
// =============================================================
// This class handles all keyword-based response matching.
// It processes user input, identifies relevant cybersecurity
// topics, and returns an appropriate educational response.
// A total of 30 distinct question topics are supported.
//
// References:
// [3] Pieterse, H. 2021. The Cyber Threat Landscape in South
//     Africa: A 10-Year Review. The African Journal of
//     Information and Communication, 28(28).
//     doi: https://doi.org/10.23962/10539/32213. [Online].
//     Available at: https://www.scielo.org.za/scielo.php?pid=
//     S2077-72132021000200003&script=sci_arttext
//     [Accessed 16 February 2026].
//
// [4] South African Banking Risk Information Centre (SABRIC),
//     2023. Cybercrime [Online].
//     Available at: https://www.sabric.co.za/stay-safe/
//     cybercrime/ [Accessed 31 March 2026].
//
// [5] National Cybersecurity Policy Framework (NCPF), 2015.
//     Department of Telecommunications and Postal Services
//     [Online]. Available at: https://www.gov.za/sites/default/
//     files/gcis_document/201512/national-cybersecurity-policy-
//     framework.pdf [Accessed 31 March 2026].
//
// [9] Information Regulator (South Africa), 2021. Protection of
//     Personal Information Act (POPIA) [Online].
//     Available at: https://www.inforegulator.org.za
//     [Accessed 31 March 2026].
//
// [10] South African Police Service (SAPS), 2024. Report
//      Cybercrime [Online]. Available at: https://www.saps.gov.za
//      [Accessed 31 March 2026].
// =============================================================

using System;

namespace CybersecurityChatbot
{
    public class ResponseEngine
    {
        // Processes raw user input and returns a matching cybersecurity response.
        // Returns null if no keyword match is found — the caller handles the fallback.
        // 30 distinct cybersecurity topics are covered below.
        public string? GetResponse(string userInput)
        {
            // Convert to lowercase for case-insensitive keyword matching
            string input = userInput.ToLower().Trim();

            // ===========================================================
            // QUESTION 1 — How are you?
            // ===========================================================
            if (input.Contains("how are you") || input.Contains("how r u") ||
                input.Contains("how do you feel") || input.Contains("are you okay"))
            {
                return "I'm fully operational and ready to help! Cybersecurity threats in " +
                       "South Africa are growing every year [3], so there's no better time " +
                       "to get informed. What topic can I help you with today?";
            }

            // ===========================================================
            // QUESTION 2 — What is your purpose?
            // ===========================================================
            if (input.Contains("your purpose") || input.Contains("what do you do") ||
                input.Contains("what are you") || input.Contains("who are you"))
            {
                return "I am a Cybersecurity Awareness Bot designed to educate South African " +
                       "citizens on identifying and avoiding online threats. South Africa is " +
                       "among the top targets for cybercrime globally [3]. I cover phishing, " +
                       "passwords, malware, privacy, and much more. Ask me anything!";
            }

            // ===========================================================
            // QUESTION 3 — What can I ask you about?
            // ===========================================================
            if (input.Contains("what can i ask") || input.Contains("topics") ||
                input.Contains("what can you help") || input.Contains("help me") ||
                input.Contains("menu"))
            {
                return "Here are the topics you can ask me about:\n" +
                       "     1.  Phishing\n" +
                       "     2.  Passwords\n" +
                       "     3.  Malware / Ransomware\n" +
                       "     4.  Safe Browsing\n" +
                       "     5.  Social Engineering\n" +
                       "     6.  Two-Factor Authentication (2FA)\n" +
                       "     7.  Public Wi-Fi\n" +
                       "     8.  Data Privacy / POPIA\n" +
                       "     9.  Identity Theft\n" +
                       "     10. Cyberbullying\n" +
                       "     11. Online Shopping Safety\n" +
                       "     12. Email Safety\n" +
                       "     13. Software Updates\n" +
                       "     14. Data Backups\n" +
                       "     15. Firewalls\n" +
                       "     16. Encryption\n" +
                       "     17. The Dark Web\n" +
                       "     18. Shoulder Surfing\n" +
                       "     19. USB / Removable Media\n" +
                       "     20. Children Online Safety\n" +
                       "     21. Online Banking Safety\n" +
                       "     22. Deepfakes and AI Scams\n" +
                       "     23. SIM Swapping\n" +
                       "     24. Account Hacking\n" +
                       "     25. Cookies and Online Tracking\n" +
                       "     26. Cloud Security\n" +
                       "     27. Reporting Cybercrime in SA\n" +
                       "     28. VPNs\n" +
                       "     29. Smishing (SMS Phishing)\n" +
                       "     30. Safe Use of Social Media\n\n" +
                       "     Just type any topic to learn more!";
            }

            // ===========================================================
            // QUESTION 4 — Phishing
            // Phishing is one of the most prevalent threats in South Africa [3]
            // ===========================================================
            if (input.Contains("phishing") || input.Contains("phish") ||
                input.Contains("fake email") || input.Contains("suspicious email"))
            {
                return "Phishing is one of the most common forms of cybercrime in South Africa [3].\n" +
                       "Attackers send fake emails pretending to be banks, SARS, or trusted services\n" +
                       "to steal your personal information.\n\n" +
                       "  Protect yourself:\n" +
                       "     - Always verify the sender's actual email address\n" +
                       "     - Never click links in unsolicited emails\n" +
                       "     - Legitimate banks will NEVER ask for your PIN via email [4]\n" +
                       "     - When in doubt, contact the organisation directly";
            }

            // ===========================================================
            // QUESTION 5 — Passwords
            // ===========================================================
            if (input.Contains("password") || input.Contains("passphrase") ||
                input.Contains("credentials") || input.Contains("strong password"))
            {
                return "Strong passwords are your first line of digital defence.\n\n" +
                       "  Tips:\n" +
                       "     - Use at least 12 characters\n" +
                       "     - Mix uppercase, lowercase, numbers, and symbols\n" +
                       "     - Never use your name, birthday, or 'password123'\n" +
                       "     - Use a unique password for every account\n" +
                       "     - Consider a reputable password manager\n" +
                       "     - Change passwords regularly, especially after a breach [4]";
            }

            // ===========================================================
            // QUESTION 6 — Malware and Ransomware
            // Malware attacks on SA infrastructure have increased significantly [3]
            // ===========================================================
            if (input.Contains("malware") || input.Contains("ransomware") ||
                input.Contains("virus") || input.Contains("spyware") ||
                input.Contains("trojan"))
            {
                return "Malware is malicious software designed to damage or gain unauthorised\n" +
                       "access to your system [3]. Ransomware locks your files and demands\n" +
                       "payment — it has heavily targeted South African businesses.\n\n" +
                       "  Protect yourself:\n" +
                       "     - Keep your antivirus software updated at all times\n" +
                       "     - Never download software from untrusted websites\n" +
                       "     - Back up your data regularly\n" +
                       "     - Do not open email attachments from strangers";
            }

            // ===========================================================
            // QUESTION 7 — Safe Browsing
            // ===========================================================
            if (input.Contains("browsing") || input.Contains("safe browser") ||
                input.Contains("internet safety") || input.Contains("https") ||
                input.Contains("url"))
            {
                return "Safe browsing habits protect you from most online threats.\n\n" +
                       "  Key habits:\n" +
                       "     - Always confirm the URL starts with 'https://'\n" +
                       "     - Look for the padlock icon in your browser bar\n" +
                       "     - Avoid clicking pop-up ads\n" +
                       "     - Keep your browser and its plugins updated\n" +
                       "     - Never enter personal data on sites you cannot verify [5]";
            }

            // ===========================================================
            // QUESTION 8 — Social Engineering
            // ===========================================================
            if (input.Contains("social engineering") || input.Contains("manipulation") ||
                input.Contains("pretexting") || input.Contains("impersonat"))
            {
                return "Social engineering exploits human trust rather than technical\n" +
                       "vulnerabilities [3]. Attackers may pose as IT staff, a manager,\n" +
                       "or a government official to get sensitive info out of you.\n\n" +
                       "  Stay safe:\n" +
                       "     - Always verify identities before sharing any information\n" +
                       "     - Be suspicious of urgent or threatening requests\n" +
                       "     - Never share passwords, even with 'IT support'\n" +
                       "     - If it feels wrong, trust your instincts and verify";
            }

            // ===========================================================
            // QUESTION 9 — Two-Factor Authentication (2FA)
            // ===========================================================
            if (input.Contains("two factor") || input.Contains("2fa") ||
                input.Contains("multi factor") || input.Contains("mfa") ||
                input.Contains("otp") || input.Contains("one time pin"))
            {
                return "Two-factor authentication (2FA) adds a second layer of security\n" +
                       "beyond your password. Even if someone steals your password, they\n" +
                       "still cannot access your account without the second factor.\n\n" +
                       "  Tips:\n" +
                       "     - Enable 2FA on email, banking, and social media\n" +
                       "     - Use an authenticator app where possible (more secure than SMS)\n" +
                       "     - Never share your OTP with anyone, ever [4]";
            }

            // ===========================================================
            // QUESTION 10 — Public Wi-Fi
            // ===========================================================
            if (input.Contains("wifi") || input.Contains("wi-fi") ||
                input.Contains("public wifi") || input.Contains("hotspot"))
            {
                return "Public Wi-Fi is a hunting ground for cybercriminals [3].\n\n" +
                       "  Rules:\n" +
                       "     - Never access online banking or emails on public Wi-Fi\n" +
                       "     - Always use a VPN to encrypt your connection\n" +
                       "     - Turn off auto-connect on your device\n" +
                       "     - Forget the network after use\n" +
                       "     - Mobile data is safer than public Wi-Fi for sensitive tasks";
            }

            // ===========================================================
            // QUESTION 11 — Data Privacy and POPIA
            // ===========================================================
            if (input.Contains("privacy") || input.Contains("popia") ||
                input.Contains("personal information") || input.Contains("data protection"))
            {
                return "Data privacy is your legal right in South Africa, protected by the\n" +
                       "Protection of Personal Information Act (POPIA) [9].\n\n" +
                       "  Protect your data:\n" +
                       "     - Read privacy policies before using apps\n" +
                       "     - Limit what personal info you share online\n" +
                       "     - Report data misuse to the Information Regulator [9]\n" +
                       "     - Regularly review app permissions on your phone";
            }

            // ===========================================================
            // QUESTION 12 — Identity Theft
            // ===========================================================
            if (input.Contains("identity theft") || input.Contains("identity fraud") ||
                input.Contains("stolen identity") || input.Contains("id theft"))
            {
                return "Identity theft happens when a criminal uses your personal details\n" +
                       "to impersonate you — opening accounts, taking loans, or committing\n" +
                       "crimes in your name [4].\n\n" +
                       "  Prevention:\n" +
                       "     - Never share your ID number online unless absolutely necessary\n" +
                       "     - Shred documents with personal info before discarding\n" +
                       "     - Regularly check your credit report for suspicious activity\n" +
                       "     - Report identity theft to SAPS immediately [10]";
            }

            // ===========================================================
            // QUESTION 13 — Cyberbullying
            // ===========================================================
            if (input.Contains("cyberbullying") || input.Contains("cyber bully") ||
                input.Contains("online harassment") || input.Contains("trolling"))
            {
                return "Cyberbullying is the use of digital platforms to harass, threaten,\n" +
                       "or humiliate someone [5].\n\n" +
                       "  What to do:\n" +
                       "     - Do not respond to the bully — it often escalates things\n" +
                       "     - Screenshot and save evidence of the harassment\n" +
                       "     - Report the behaviour to the platform and to SAPS [10]\n" +
                       "     - Talk to a trusted adult or counsellor if you feel threatened";
            }

            // ===========================================================
            // QUESTION 14 — Online Shopping Safety
            // ===========================================================
            if (input.Contains("online shopping") || input.Contains("shopping online") ||
                input.Contains("e-commerce") || input.Contains("buy online"))
            {
                return "Online shopping fraud is among the fastest growing cybercrimes in SA [4].\n\n" +
                       "  Shop safely:\n" +
                       "     - Only buy from established, trusted retailers\n" +
                       "     - Check that the site uses 'https://' and has a padlock\n" +
                       "     - Use a credit card or PayPal rather than direct bank transfer\n" +
                       "     - Be suspicious of deals that seem too good to be true\n" +
                       "     - Keep your purchase confirmation emails as proof";
            }

            // ===========================================================
            // QUESTION 15 — Email Safety (General)
            // ===========================================================
            if (input.Contains("email safety") || input.Contains("email security") ||
                input.Contains("safe email") || input.Contains("email tips"))
            {
                return "Email is the most commonly exploited communication channel by\n" +
                       "cybercriminals in South Africa [3].\n\n" +
                       "  Email safety tips:\n" +
                       "     - Never open attachments from unknown senders\n" +
                       "     - Hover over links to preview the actual URL before clicking\n" +
                       "     - Be cautious of emails creating urgency or fear\n" +
                       "     - Enable spam filtering on your email account\n" +
                       "     - Use a separate email address for online sign-ups";
            }

            // ===========================================================
            // QUESTION 16 — Software Updates and Patching
            // ===========================================================
            if (input.Contains("update") || input.Contains("software update") ||
                input.Contains("patch") || input.Contains("outdated software"))
            {
                return "Unpatched software is one of the most common entry points for\n" +
                       "attackers. Updates fix known security vulnerabilities [5].\n\n" +
                       "  Good habits:\n" +
                       "     - Enable automatic updates on your OS and apps\n" +
                       "     - Update your browser and antivirus regularly\n" +
                       "     - Do not ignore update notifications — they are often urgent\n" +
                       "     - Remove software you no longer use";
            }

            // ===========================================================
            // QUESTION 17 — Data Backups
            // ===========================================================
            if (input.Contains("backup") || input.Contains("back up") ||
                input.Contains("data recovery") || input.Contains("restore data"))
            {
                return "Regular backups are your safety net against ransomware and data loss.\n\n" +
                       "  Best practices:\n" +
                       "     - Follow the 3-2-1 rule: 3 copies, 2 different media, 1 offsite\n" +
                       "     - Back up to both an external drive and the cloud\n" +
                       "     - Test your backups regularly to confirm they restore correctly\n" +
                       "     - Automate backups so you never forget [5]";
            }

            // ===========================================================
            // QUESTION 18 — Firewalls
            // ===========================================================
            if (input.Contains("firewall") || input.Contains("fire wall") ||
                input.Contains("network protection"))
            {
                return "A firewall monitors and controls incoming and outgoing network\n" +
                       "traffic based on predefined security rules [5].\n\n" +
                       "  Tips:\n" +
                       "     - Always keep your operating system's firewall turned on\n" +
                       "     - Use a hardware firewall on your home router if possible\n" +
                       "     - Never disable your firewall, even temporarily\n" +
                       "     - Business users should configure firewall rules carefully";
            }

            // ===========================================================
            // QUESTION 19 — Encryption
            // ===========================================================
            if (input.Contains("encryption") || input.Contains("encrypt") ||
                input.Contains("encrypted") || input.Contains("end-to-end"))
            {
                return "Encryption converts your data into unreadable code that only\n" +
                       "authorised parties can decode — a fundamental security layer [5].\n\n" +
                       "  Where to use encryption:\n" +
                       "     - Use messaging apps with end-to-end encryption (Signal, WhatsApp)\n" +
                       "     - Enable full-disk encryption on your laptop and phone\n" +
                       "     - Confirm websites use HTTPS (TLS encryption)\n" +
                       "     - Encrypt sensitive files before emailing them";
            }

            // ===========================================================
            // QUESTION 20 — The Dark Web
            // ===========================================================
            if (input.Contains("dark web") || input.Contains("darkweb") ||
                input.Contains("deep web") || input.Contains("tor browser"))
            {
                return "The dark web is a hidden portion of the internet not indexed by\n" +
                       "standard search engines. It is used for both privacy and criminal\n" +
                       "activity [3].\n\n" +
                       "  What you should know:\n" +
                       "     - Stolen personal data is often sold there after breaches\n" +
                       "     - Check if your data was leaked at haveibeenpwned.com\n" +
                       "     - Avoid the dark web — the risks are significant\n" +
                       "     - Law enforcement actively monitors it for criminal activity [10]";
            }

            // ===========================================================
            // QUESTION 21 — Shoulder Surfing
            // ===========================================================
            if (input.Contains("shoulder surf") || input.Contains("shoulder-surf") ||
                input.Contains("someone watching") || input.Contains("spying on screen"))
            {
                return "Shoulder surfing is when someone physically watches your screen\n" +
                       "or keyboard to steal PINs, passwords, or sensitive information.\n\n" +
                       "  Prevention:\n" +
                       "     - Shield your screen and keypad when entering a PIN at an ATM\n" +
                       "     - Use a privacy screen protector in public spaces\n" +
                       "     - Be aware of your surroundings when working in public\n" +
                       "     - Log out of sensitive accounts as soon as you are done [4]";
            }

            // ===========================================================
            // QUESTION 22 — USB and Removable Media
            // ===========================================================
            if (input.Contains("usb") || input.Contains("flash drive") ||
                input.Contains("removable media") || input.Contains("thumb drive"))
            {
                return "USB drives can carry malware and introduce it directly into your\n" +
                       "system — even without an internet connection [3].\n\n" +
                       "  Safe usage:\n" +
                       "     - Never plug in a USB drive you found or received unexpectedly\n" +
                       "     - Scan all external drives with antivirus before opening files\n" +
                       "     - Disable USB autorun on your operating system\n" +
                       "     - Use encrypted USB drives for sensitive data";
            }

            // ===========================================================
            // QUESTION 23 — Children Online Safety
            // ===========================================================
            if (input.Contains("children") || input.Contains("kids online") ||
                input.Contains("child safety") || input.Contains("parental control"))
            {
                return "Children are particularly vulnerable to online threats including\n" +
                       "grooming, cyberbullying, and inappropriate content [5].\n\n" +
                       "  How to protect children:\n" +
                       "     - Enable parental controls on devices and browsers\n" +
                       "     - Keep devices in shared family spaces, not bedrooms\n" +
                       "     - Teach children never to share personal info with strangers\n" +
                       "     - Have open conversations about online safety\n" +
                       "     - Know which platforms your children are using";
            }

            // ===========================================================
            // QUESTION 24 — Online Banking Safety
            // ===========================================================
            if (input.Contains("online banking") || input.Contains("internet banking") ||
                input.Contains("bank app") || input.Contains("banking safety"))
            {
                return "Online banking fraud costs South Africa billions annually [4].\n\n" +
                       "  Stay safe:\n" +
                       "     - Only access banking on your personal, secured device\n" +
                       "     - Type your bank's URL directly — never click emailed links\n" +
                       "     - Enable transaction notifications on your account\n" +
                       "     - Log out completely after every banking session\n" +
                       "     - Report suspicious transactions to your bank immediately [4]";
            }

            // ===========================================================
            // QUESTION 25 — Deepfakes and AI Scams
            // ===========================================================
            if (input.Contains("deepfake") || input.Contains("deep fake") ||
                input.Contains("ai scam") || input.Contains("fake video") ||
                input.Contains("voice cloning"))
            {
                return "Deepfakes use AI to create convincing fake videos, images, or voice\n" +
                       "recordings of real people — increasingly used in fraud [3].\n\n" +
                       "  What to watch for:\n" +
                       "     - Unnatural blinking, skin texture, or lip sync in videos\n" +
                       "     - Unexpected voice calls from 'family' asking for money\n" +
                       "     - Always verify urgent requests through a second channel\n" +
                       "     - Be sceptical of viral content that seems too shocking to be real";
            }

            // ===========================================================
            // QUESTION 26 — SIM Swapping
            // ===========================================================
            if (input.Contains("sim swap") || input.Contains("sim swapping") ||
                input.Contains("sim hijack") || input.Contains("sim card fraud"))
            {
                return "SIM swapping is when criminals convince your network to transfer\n" +
                       "your number to a SIM they control, granting access to your OTPs [4].\n\n" +
                       "  Protection:\n" +
                       "     - Set a SIM swap PIN or block with your network provider\n" +
                       "     - Use an authenticator app instead of SMS-based 2FA\n" +
                       "     - Contact your provider immediately if your signal disappears\n" +
                       "     - Monitor your bank accounts for unexpected changes [4]";
            }

            // ===========================================================
            // QUESTION 27 — Account Hacking
            // ===========================================================
            if (input.Contains("hacked") || input.Contains("account hack") ||
                input.Contains("my account was") || input.Contains("compromised account"))
            {
                return "If your account has been hacked, act immediately.\n\n" +
                       "  Steps to take:\n" +
                       "     - Change your password immediately from a secure device\n" +
                       "     - Enable two-factor authentication right away\n" +
                       "     - Check for unrecognised logins in your account activity\n" +
                       "     - Warn contacts in case the attacker messaged them\n" +
                       "     - Report the incident to the platform and to SAPS [10]";
            }

            // ===========================================================
            // QUESTION 28 — Cookies and Online Tracking
            // ===========================================================
            if (input.Contains("cookie") || input.Contains("tracking") ||
                input.Contains("online tracking") || input.Contains("browser history"))
            {
                return "Cookies are small files websites use to track your browsing\n" +
                       "behaviour and remember preferences [9].\n\n" +
                       "  Protect your privacy:\n" +
                       "     - Decline non-essential cookies when prompted\n" +
                       "     - Regularly clear your browser cookies and cache\n" +
                       "     - Use browser extensions that block trackers\n" +
                       "     - Use private/incognito mode for sensitive browsing\n" +
                       "     - Under POPIA you have rights over how your data is used [9]";
            }

            // ===========================================================
            // QUESTION 29 — Cloud Security
            // ===========================================================
            if (input.Contains("cloud") || input.Contains("cloud storage") ||
                input.Contains("google drive") || input.Contains("onedrive") ||
                input.Contains("dropbox"))
            {
                return "Cloud storage is convenient but carries security risks if not\n" +
                       "properly configured [5].\n\n" +
                       "  Best practices:\n" +
                       "     - Use strong, unique passwords for all cloud accounts\n" +
                       "     - Enable 2FA on all cloud services\n" +
                       "     - Do not store unencrypted sensitive documents in the cloud\n" +
                       "     - Review sharing permissions — never share folders publicly\n" +
                       "     - Regularly audit which third-party apps have access";
            }

            // ===========================================================
            // QUESTION 30 — Reporting Cybercrime in South Africa
            // ===========================================================
            if (input.Contains("report") || input.Contains("cybercrime report") ||
                input.Contains("report crime") || input.Contains("where to report"))
            {
                return "Reporting cybercrime is essential — it helps law enforcement track\n" +
                       "criminal networks targeting South Africans [10].\n\n" +
                       "  How to report:\n" +
                       "     - Contact SAPS at your nearest police station or call 10111 [10]\n" +
                       "     - Report banking fraud to your bank's dedicated fraud hotline\n" +
                       "     - Report data breaches to the Information Regulator:\n" +
                       "       complaints.IR@justice.gov.za [9]\n" +
                       "     - Report scams to the South African Fraud Prevention Service (SAFPS)\n" +
                       "     - Always keep records: screenshots, emails, and transaction details";
            }

            // ===========================================================
            // BONUS — VPNs (linked to Q7 but common standalone query)
            // ===========================================================
            if (input.Contains("vpn") || input.Contains("virtual private network"))
            {
                return "A VPN encrypts your internet traffic and hides your IP address,\n" +
                       "making it much harder for attackers to intercept your data [5].\n\n" +
                       "  Tips:\n" +
                       "     - Use a reputable, paid VPN service (free VPNs often collect your data)\n" +
                       "     - Always connect via VPN when using public Wi-Fi\n" +
                       "     - A VPN adds privacy but does not make you fully anonymous";
            }

            // ===========================================================
            // BONUS — Smishing / SMS Phishing (distinct from email phishing)
            // ===========================================================
            if (input.Contains("smishing") || input.Contains("sms scam") ||
                input.Contains("text message scam") || input.Contains("fake sms"))
            {
                return "Smishing is phishing via SMS. Attackers send fake text messages\n" +
                       "pretending to be banks, SARS, or courier services [3].\n\n" +
                       "  Stay safe:\n" +
                       "     - Never click links in unexpected SMS messages\n" +
                       "     - Verify directly with the organisation via their official number\n" +
                       "     - SARS and banks will never send account links via SMS [4]\n" +
                       "     - Delete suspicious messages immediately";
            }

            // ===========================================================
            // BONUS — Safe Use of Social Media
            // ===========================================================
            if (input.Contains("social media") || input.Contains("facebook") ||
                input.Contains("instagram") || input.Contains("twitter") ||
                input.Contains("tiktok") || input.Contains("linkedin"))
            {
                return "Social media platforms expose personal data that criminals exploit\n" +
                       "for social engineering and targeted attacks [3].\n\n" +
                       "  Safe social media habits:\n" +
                       "     - Set your profiles to private\n" +
                       "     - Never share your location, ID number, or financial details\n" +
                       "     - Be selective about friend or connection requests\n" +
                       "     - Avoid sharing travel plans — it signals your home is empty\n" +
                       "     - Review which third-party apps have access to your account";
            }

            // No keyword match — return null so ChatBot.cs displays the fallback message
            return null;
        }
    }
}
