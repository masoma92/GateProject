import { getInfoData } from './infodata';

export class InfoData{
    path: string;
    isBack: boolean;
    isLogin: boolean;
    text: string[] = [];

    constructor(obj: InfoData){
        this.path = obj.path;
        this.isBack = obj.isBack;
        this.isLogin = obj.isLogin;
        if (obj.text != null){
            obj.text.forEach(x => {
                this.text.push(x);
            });
        }
    }

    static getData(path: string) {
        let infoData = getInfoData();
        let result = new InfoData(infoData.find(x => x.path == path) as InfoData);
        return result;
    }
}