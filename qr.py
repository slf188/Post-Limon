import qrcode
# crear codigo qr del video
video = qrcode.make("https://www.youtube.com/watch?v=l2e7CClw6Ds")
video.save("video_qr.png")
