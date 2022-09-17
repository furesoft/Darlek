(open 'ObjectModel)

(define mycrawler (lambda (args) (
	(define result (make-object))
	(set-property result 'title "hello scheme")

	result
))

(register-crawler "www.sample.com" mycrawler)