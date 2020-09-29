export class JsonInHttpHeader {

    public static encode(textToEncode: string) {

        var result = "";
        for (var i = 0; i < textToEncode.length; i++) {

            var ch = textToEncode[i];
            var chCode = textToEncode.charCodeAt(i);

            if (chCode >= 0 && chCode <= 126 && ch != '\\') {
                result += ch;
            } else {
                var encodedCh = chCode.toString(16).toUpperCase();

                while (encodedCh.length < 4) {
                    encodedCh = `0${encodedCh}`;
                }

                result += '\\u' + encodedCh;
            }

        }
        return result;
    }

    public static decode(textToDecode) {

        var borderCharPosition;
        var hexaDecimal;
        var originalChar;

        while(textToDecode.indexOf("\\") != -1){
            borderCharPosition = textToDecode.indexOf("\\");
            hexaDecimal = textToDecode.substring(borderCharPosition + 2, borderCharPosition + 6);
            originalChar = String.fromCharCode(parseInt(hexaDecimal, 16));
            textToDecode = textToDecode.replace(`\\u${hexaDecimal}`, originalChar );
        }
        return textToDecode;
    }
}