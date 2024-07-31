window.cookieHelper = {
    setCookieWithExpiry: function (name, value, days) {
        let expires = "";
        if (days) {
            let date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "") + expires + "; path=/";
    },

    setCookie: function (name, value) {
        document.cookie = name + "=" + value || "" + "; path=/";
    },

    // 获取cookie
    getCookie: function (name) {
        let nameEQ = name + "=";
        let ca = document.cookie.split(';');
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) === ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    },

    // 删除cookie
    deleteCookie: function (name) {
        document.cookie = name + '=; Max-Age=-99999999;';
    }
};
