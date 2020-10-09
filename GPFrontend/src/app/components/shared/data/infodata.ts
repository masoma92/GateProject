import { InfoData } from './info.data';

export function getInfoData(): InfoData[] {
    return infoData;
}

var infoData: InfoData[] = [
    {
        "path": "register-success",
        "isBack": true,
        "isLogin": false,
        "text": [
             "Thanks for signing up for GateProject!",
             "We have sent an email with a confirmation link to [email]. In order to complete the sign-up process, please click the confirmation link.",
             "If you do not receive a confirmation email, please check your spam folder. Also, please verify that you entered a valid email address in our sign-up form."
        ]
    },
    {
        "path": "forget-password-success",
        "isBack": false,
        "isLogin": true,
        "text": [
             "Password reset completed!"
        ]
    },
    {
        "path": "forget-password-requested",
        "isBack": true,
        "isLogin": false,
        "text": [
             "We have sent an email with a password-reset link to [email]. In order to reset your password, please click the link.",
             "If you do not receive a confirmation email, please check your spam folder. Also, please verify that you entered a valid email address in our sign-up form."
        ]
    },
]